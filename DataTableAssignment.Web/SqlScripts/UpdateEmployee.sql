CREATE OR ALTER PROCEDURE UpdateEmployee
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @Position NVARCHAR(100),
    @Office NVARCHAR(100),
    @Age INT,
    @Salary INT
AS
BEGIN
    UPDATE Employees
    SET Name = @Name,
        Position = @Position,
        Office = @Office,
        Age = @Age,
        Salary = @Salary
    WHERE Id = @Id
END