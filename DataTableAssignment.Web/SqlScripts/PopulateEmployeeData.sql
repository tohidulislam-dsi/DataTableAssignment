INSERT INTO EmployeeDetails (EmployeeId, Address, PhoneNumber)
SELECT Id, 'Default Address', '000-000-0000'
FROM Employees;

INSERT INTO EmployeeBenefits (EmployeeDetailId, BenefitType, BenefitValue)
SELECT ed.Id, 'Health Insurance', 1000
FROM EmployeeDetails ed;
