CREATE OR ALTER PROCEDURE GetFilteredEmployees
    @SearchValue NVARCHAR(100),
    @Start INT,
    @Length INT,
    @OrderBy NVARCHAR(MAX),
    @Name NVARCHAR(100) = NULL,
    @Position NVARCHAR(100) = NULL,
    @Office NVARCHAR(100) = NULL,
    @Age INT = NULL,
    @Salary INT = NULL,
    @TotalEmployees INT OUTPUT,
    @TotalFilteredRecords INT OUTPUT
AS
BEGIN
    -- Total employees count (unfiltered)
    SELECT @TotalEmployees = COUNT(*) FROM Employees;

    -- Escape '%' and '_' in search values
    SET @SearchValue = REPLACE(REPLACE(@SearchValue, '[', '[[]'), '%', '[%]');
    SET @SearchValue = REPLACE(@SearchValue, '_', '[_]');

    SET @Name = REPLACE(REPLACE(@Name, '[', '[[]'), '%', '[%]');
    SET @Name = REPLACE(@Name, '_', '[_]');

    SET @Position = REPLACE(REPLACE(@Position, '[', '[[]'), '%', '[%]');
    SET @Position = REPLACE(@Position, '_', '[_]');

    SET @Office = REPLACE(REPLACE(@Office, '[', '[[]'), '%', '[%]');
    SET @Office = REPLACE(@Office, '_', '[_]');


    -- Construct dynamic SQL for filtering, ordering, and pagination
    DECLARE @SQL NVARCHAR(MAX) = N'
    WITH FilteredEmployees AS (
        SELECT *,
               COUNT(*) OVER () AS TotalFilteredRecords
        FROM Employees
        WHERE (@SearchValue IS NULL OR
               Name LIKE ''%'' + @SearchValue + ''%'' ESCAPE ''\'' OR
               Position LIKE ''%'' + @SearchValue + ''%'' ESCAPE ''\'' OR
               Office LIKE ''%'' + @SearchValue + ''%'' ESCAPE ''\'' OR
               CAST(Age AS NVARCHAR) LIKE ''%'' + @SearchValue + ''%'' ESCAPE ''\'' OR
               CAST(Salary AS NVARCHAR) LIKE ''%'' + @SearchValue + ''%'' ESCAPE ''\'')
          AND (@Name IS NULL OR Name LIKE ''%'' + @Name + ''%'' ESCAPE ''\'')
          AND (@Position IS NULL OR Position LIKE ''%'' + @Position + ''%'' ESCAPE ''\'')
          AND (@Office IS NULL OR Office LIKE ''%'' + @Office + ''%'' ESCAPE ''\'')
          AND (@Age IS NULL OR Age = @Age)
          AND (@Salary IS NULL OR Salary = @Salary)
    )
    SELECT *
    FROM FilteredEmployees
    ORDER BY ' + @OrderBy + '
    OFFSET @Start ROWS FETCH NEXT @Length ROWS ONLY

    -- Query the CTE again to get the total filtered records
    SELECT @TotalFilteredRecords = MAX(TotalFilteredRecords)
    FROM FilteredEmployees;';

    -- Define parameter types for sp_executesql
    DECLARE @ParamDefinition NVARCHAR(MAX) = N'
        @SearchValue NVARCHAR(100),
        @Name NVARCHAR(100),
        @Position NVARCHAR(100),
        @Office NVARCHAR(100),
        @Age INT,
        @Salary INT,
        @Start INT,
        @Length INT,
        @TotalFilteredRecords INT OUTPUT';

    -- Execute the dynamic SQL
    EXEC sp_executesql
        @SQL,
        @ParamDefinition,
        @SearchValue,
        @Name,
        @Position,
        @Office,
        @Age,
        @Salary,
        @Start,
        @Length,
        @TotalFilteredRecords OUTPUT;
END;
