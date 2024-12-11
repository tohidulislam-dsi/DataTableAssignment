CREATE PROCEDURE AddEmployee
    @Name NVARCHAR(100),
    @Position NVARCHAR(100),
    @Office NVARCHAR(100),
    @Age INT,
    @Salary INT
AS
BEGIN
    INSERT INTO Employees (Name, Position, Office, Age, Salary)
    VALUES (@Name, @Position, @Office, @Age, @Salary)
END