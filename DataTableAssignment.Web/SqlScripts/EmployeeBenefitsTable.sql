CREATE TABLE EmployeeBenefits (
    Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    EmployeeDetailId UNIQUEIDENTIFIER NOT NULL,
    BenefitType NVARCHAR(MAX),
    BenefitValue INT,
    FOREIGN KEY (EmployeeDetailId) REFERENCES EmployeeDetails(Id)
);