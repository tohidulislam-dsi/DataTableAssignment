CREATE OR ALTER PROCEDURE GetEmployeeBenefitsByEmployeeDetailsId
    @EmployeeDetailId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        EmployeeDetailId,
        BenefitType,
        BenefitValue
    FROM
        EmployeeBenefits
    WHERE
        EmployeeDetailId = @EmployeeDetailId;
END;