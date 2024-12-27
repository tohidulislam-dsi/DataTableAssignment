CREATE OR ALTER PROCEDURE AddEmployee
    @Name NVARCHAR(100),
    @Position NVARCHAR(100),
    @Office NVARCHAR(100),
    @Age INT,
    @Salary INT,
    @Id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN


    INSERT INTO Employees (Id, Name, Position, Office, Age, Salary)
    VALUES (@Id, @Name, @Position, @Office, @Age, @Salary);

    SELECT @Id AS Id;
END
