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