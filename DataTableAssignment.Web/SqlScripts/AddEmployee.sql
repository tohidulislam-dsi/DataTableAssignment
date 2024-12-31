CREATE OR ALTER PROCEDURE AddEmployee
    @Name NVARCHAR(100),
    @Position NVARCHAR(100),
    @Office NVARCHAR(100),
    @Age INT,
    @Salary INT,
    @Id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare a table variable to capture the output
    DECLARE @OutputTable TABLE (Id UNIQUEIDENTIFIER);

    INSERT INTO Employees (Name, Position, Office, Age, Salary)
    OUTPUT INSERTED.Id INTO @OutputTable
    VALUES (@Name, @Position, @Office, @Age, @Salary);

    -- Retrieve the inserted Id
    SELECT @Id = Id FROM @OutputTable;
END
