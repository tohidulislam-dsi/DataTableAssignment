CREATE OR ALTER PROCEDURE GetEmployeeById
    @EmployeeId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        e.Id AS Id,
        e.Name AS Name,
        e.Position AS Position,
        e.Office AS Office,
        e.Age AS Age,
        e.Salary AS Salary,
        e.CreatedOn AS CreatedOn,
        ed.Id AS EmployeeDetailsId,
        ed.Address AS Address,
        ed.PhoneNumber AS PhoneNumber,
        ed.CreatedOn AS EmployeeDetailsCreatedOn,
        eb.Id AS EmployeeBenefitsId,
        eb.BenefitType AS BenefitType,
        eb.BenefitValue AS BenefitValue,
        eb.CreatedOn AS EmployeeBenefitsCreatedOn
    FROM Employees e
    LEFT JOIN EmployeeDetails ed ON e.Id = ed.EmployeeId
    LEFT JOIN EmployeeBenefits eb ON ed.Id = eb.EmployeeDetailId
    WHERE e.Id = @EmployeeId;
END