CREATE PROCEDURE DeleteEmployeeInformation
    @EmployeeId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Delete EmployeeBenefits
        DELETE FROM EmployeeBenefits
        WHERE EmployeeDetailId IN (SELECT Id FROM EmployeeDetails WHERE EmployeeId = @EmployeeId);

        -- Delete EmployeeDetails
        DELETE FROM EmployeeDetails
        WHERE EmployeeId = @EmployeeId;

        -- Delete Employee
        DELETE FROM Employees
        WHERE Id = @EmployeeId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END;