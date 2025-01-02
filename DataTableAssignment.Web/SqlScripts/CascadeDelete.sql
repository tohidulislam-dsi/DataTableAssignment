-- For Employee -> EmployeeDetails
ALTER TABLE EmployeeDetails
DROP CONSTRAINT FK_EmployeeDetails_Employee;

ALTER TABLE EmployeeDetails
ADD CONSTRAINT FK_EmployeeDetails_Employee
FOREIGN KEY (EmployeeId)
REFERENCES Employee(Id)
ON DELETE CASCADE;

-- For EmployeeDetails -> EmployeeBenefits
ALTER TABLE EmployeeBenefits
DROP CONSTRAINT FK_EmployeeBenefits_EmployeeDetails;

ALTER TABLE EmployeeBenefits
ADD CONSTRAINT FK_EmployeeBenefits_EmployeeDetails
FOREIGN KEY (EmployeeDetailId)
REFERENCES EmployeeDetails(Id)
ON DELETE CASCADE;
