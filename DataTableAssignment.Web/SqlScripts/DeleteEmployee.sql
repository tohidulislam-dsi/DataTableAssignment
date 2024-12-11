CREATE PROCEDURE DeleteEmployee
    @Id BIGINT
AS
BEGIN
    DELETE FROM Employees
    WHERE Id = @Id
END