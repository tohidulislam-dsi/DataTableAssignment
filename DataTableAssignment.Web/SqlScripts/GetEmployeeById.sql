CREATE OR ALTER PROCEDURE GetEmployeeById
    @Id uniqueidentifier
AS
BEGIN
    SELECT * FROM Employees WHERE Id = @Id
END