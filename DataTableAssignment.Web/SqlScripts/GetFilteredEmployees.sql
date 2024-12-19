CREATE OR ALTER PROCEDURE GetFilteredEmployees
    @EmployeeFilterData dbo.EmployeeFilterType READONLY,
    @TotalEmployees INT OUTPUT
AS
BEGIN
    -- Total employees count (unfiltered)
    SELECT @TotalEmployees = COUNT(*) FROM Employees;

    -- Extract parameters from the table-valued parameter
    DECLARE @SearchValue NVARCHAR(100);
    DECLARE @Start INT;
    DECLARE @Length INT;
    DECLARE @OrderBy NVARCHAR(MAX);
    DECLARE @Name NVARCHAR(100);
    DECLARE @Position NVARCHAR(100);
    DECLARE @Office NVARCHAR(100);
    DECLARE @Age INT;
    DECLARE @Salary INT;

    SELECT
        @SearchValue = SearchValue,
        @Start = Start,
        @Length = Length,
        @OrderBy = OrderBy,
        @Name = Name,
        @Position = Position,
        @Office = Office,
        @Age = Age,
        @Salary = Salary
    FROM @EmployeeFilterData;

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
    OFFSET @Start ROWS FETCH NEXT @Length ROWS ONLY;';

    -- Define parameter types for sp_executesql
    DECLARE @ParamDefinition NVARCHAR(MAX) = N'
        @SearchValue NVARCHAR(100),
        @Name NVARCHAR(100),
        @Position NVARCHAR(100),
        @Office NVARCHAR(100),
        @Age INT,
        @Salary INT,
        @Start INT,
        @Length INT';

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
        @Length;
END;
