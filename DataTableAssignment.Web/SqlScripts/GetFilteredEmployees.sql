CREATE PROCEDURE GetFilteredEmployees
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
    SELECT @TotalEmployees = COUNT(*) FROM Employees;

    SELECT @TotalFilteredRecords = COUNT(*)
    FROM Employees
    WHERE (@SearchValue IS NULL OR
           Name LIKE '%' + @SearchValue + '%' OR
           Position LIKE '%' + @SearchValue + '%' OR
           Office LIKE '%' + @SearchValue + '%' OR
           CAST(Age AS NVARCHAR) LIKE '%' + @SearchValue + '%' OR
           CAST(Salary AS NVARCHAR) LIKE '%' + @SearchValue + '%')
      AND (@Name IS NULL OR Name LIKE '%' + @Name + '%')
      AND (@Position IS NULL OR Position LIKE '%' + @Position + '%')
      AND (@Office IS NULL OR Office LIKE '%' + @Office + '%')
      AND (@Age IS NULL OR Age = @Age)
      AND (@Salary IS NULL OR Salary = @Salary);

    DECLARE @SQL NVARCHAR(MAX) = N'
    SELECT *
    FROM Employees
    WHERE (@SearchValue IS NULL OR
           Name LIKE ''%'' + @SearchValue + ''%'' OR
           Position LIKE ''%'' + @SearchValue + ''%'' OR
           Office LIKE ''%'' + @SearchValue + ''%'' OR
           CAST(Age AS NVARCHAR) LIKE ''%'' + @SearchValue + ''%'' OR
           CAST(Salary AS NVARCHAR) LIKE ''%'' + @SearchValue + ''%'')
      AND (@Name IS NULL OR Name LIKE ''%'' + @Name + ''%'')
      AND (@Position IS NULL OR Position LIKE ''%'' + @Position + ''%'')
      AND (@Office IS NULL OR Office LIKE ''%'' + @Office + ''%'')
      AND (@Age IS NULL OR Age = @Age)
      AND (@Salary IS NULL OR Salary = @Salary)
    ORDER BY ' + @OrderBy + '
    OFFSET @Start ROWS FETCH NEXT @Length ROWS ONLY';

    EXEC sp_executesql @SQL,
        N'@SearchValue NVARCHAR(100), @Start INT, @Length INT, @Name NVARCHAR(100), @Position NVARCHAR(100), @Office NVARCHAR(100), @Age INT, @Salary INT',
        @SearchValue, @Start, @Length, @Name, @Position, @Office, @Age, @Salary;

    SELECT @TotalEmployees AS TotalEmployees, @TotalFilteredRecords AS TotalFilteredRecords;
END