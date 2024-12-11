CREATE PROCEDURE CountTotalEmployees
    @TotalEmployees INT OUTPUT
AS
BEGIN
    SELECT @TotalEmployees = COUNT(*) FROM Employees
END