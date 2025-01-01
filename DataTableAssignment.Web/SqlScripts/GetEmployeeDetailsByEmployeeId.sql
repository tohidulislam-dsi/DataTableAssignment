CREATE OR ALTER PROCEDURE GetEmployeeDetailsByEmployeeId
    @EmployeeId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        EmployeeId,
        Address,
        PhoneNumber
    FROM
        EmployeeDetails
    WHERE
        EmployeeId = @EmployeeId;
END;