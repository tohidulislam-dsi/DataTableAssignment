CREATE OR ALTER PROCEDURE GetFilteredEmployees
    @SearchValue NVARCHAR(100),
    @Start INT,
    @Length INT,
    @OrderBy NVARCHAR(MAX),
    @Name NVARCHAR(MAX) = NULL,
    @Position NVARCHAR(MAX) = NULL,
    @Office NVARCHAR(MAX) = NULL,
    @Age INT = NULL,
    @Salary INT = NULL,
    @TotalEmployees INT OUTPUT,
    @TotalFilteredRecords INT OUTPUT
AS
BEGIN
    -- Total employees count (unfiltered)
    SELECT @TotalEmployees = COUNT(*) FROM Employees;

    -- Create a temporary table to store filtered employees
    CREATE TABLE #FilteredEmployees (
        Id BIGINT,       -- Replace these with actual column names
        Name NVARCHAR(MAX),
        Position NVARCHAR(MAX),
        Office NVARCHAR(MAX),
        Age INT,
        Salary INT
        -- Add other columns as needed
    );

    -- Escape '%' and '_' in search values
    SET @SearchValue = REPLACE(REPLACE(@SearchValue, '[', '[[]'), '%', '[%]');
    SET @SearchValue = REPLACE(@SearchValue, '_', '[_]');

    SET @Name = REPLACE(REPLACE(@Name, '[', '[[]'), '%', '[%]');
    SET @Name = REPLACE(@Name, '_', '[_]');

    SET @Position = REPLACE(REPLACE(@Position, '[', '[[]'), '%', '[%]');
    SET @Position = REPLACE(@Position, '_', '[_]');

    SET @Office = REPLACE(REPLACE(@Office, '[', '[[]'), '%', '[%]');
    SET @Office = REPLACE(@Office, '_', '[_]');

    -- Populate the temporary table with filtered data
    INSERT INTO #FilteredEmployees
    SELECT *
    FROM Employees
    WHERE (@SearchValue IS NULL OR
           Name LIKE '%' + @SearchValue + '%' ESCAPE '\' OR
           Position LIKE '%' + @SearchValue + '%' ESCAPE '\' OR
           Office LIKE '%' + @SearchValue + '%' ESCAPE '\' OR
           CAST(Age AS NVARCHAR) LIKE '%' + @SearchValue + '%' ESCAPE '\' OR
           CAST(Salary AS NVARCHAR) LIKE '%' + @SearchValue + '%' ESCAPE '\')
      AND (@Name IS NULL OR Name LIKE '%' + @Name + '%' ESCAPE '\')
      AND (@Position IS NULL OR Position LIKE '%' + @Position + '%' ESCAPE '\')
      AND (@Office IS NULL OR Office LIKE '%' + @Office + '%' ESCAPE '\')
      AND (@Age IS NULL OR Age = @Age)
      AND (@Salary IS NULL OR Salary = @Salary);

    -- Get total filtered record count
    SELECT @TotalFilteredRecords = COUNT(*) FROM #FilteredEmployees;

    -- Construct dynamic SQL for pagination
    DECLARE @SQL NVARCHAR(MAX) = N'
    SELECT *
    FROM #FilteredEmployees
    ORDER BY ' + @OrderBy + '
    OFFSET @Start ROWS FETCH NEXT @Length ROWS ONLY';

    -- Execute the paginated query
    EXEC sp_executesql @SQL,
        N'@Start INT, @Length INT',
        @Start, @Length;

    -- Drop the temporary table to clean up
    DROP TABLE #FilteredEmployees;
END;
