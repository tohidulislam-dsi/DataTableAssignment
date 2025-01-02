CREATE OR ALTER PROCEDURE GetEmployeeById
    @EmployeeId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        e.Id AS EmployeeId,
        e.Name AS EmployeeName,
        e.Position AS EmployeePosition,
        e.Office AS EmployeeOffice,
        e.Age AS EmployeeAge,
        e.Salary AS EmployeeSalary,
        e.CreatedOn AS EmployeeCreatedOn,
        ed.Id AS EmployeeDetailsId,
        ed.Address AS EmployeeDetailsAddress,
        ed.PhoneNumber AS EmployeeDetailsPhoneNumber,
        ed.CreatedOn AS EmployeeDetailsCreatedOn,
        eb.Id AS EmployeeBenefitsId,
        eb.BenefitType AS EmployeeBenefitsBenefitType,
        eb.BenefitValue AS EmployeeBenefitsBenefitValue,
        eb.CreatedOn AS EmployeeBenefitsCreatedOn
    FROM Employees e
    LEFT JOIN EmployeeDetails ed ON e.Id = ed.EmployeeId
    LEFT JOIN EmployeeBenefits eb ON ed.Id = eb.EmployeeDetailId
    WHERE e.Id = @EmployeeId;
END