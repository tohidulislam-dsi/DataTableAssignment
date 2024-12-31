CREATE PROCEDURE InsertEmployeeData
    @Name NVARCHAR(MAX),
    @Position NVARCHAR(MAX),
    @Office NVARCHAR(MAX),
    @Age INT,
    @Salary INT,
    @Address NVARCHAR(MAX),
    @PhoneNumber NVARCHAR(MAX),
    @BenefitType NVARCHAR(MAX),
    @BenefitValue INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Table variables to capture inserted Ids
    DECLARE @InsertedEmployee TABLE (Id UNIQUEIDENTIFIER);
    DECLARE @InsertedEmployeeDetail TABLE (Id UNIQUEIDENTIFIER);

    -- Insert into Employees and capture the Id
    INSERT INTO dbo.Employees (Name, Position, Office, Age, Salary)
    OUTPUT INSERTED.Id INTO @InsertedEmployee
    VALUES (@Name, @Position, @Office, @Age, @Salary);

    -- Retrieve the EmployeeId
    DECLARE @EmployeeId UNIQUEIDENTIFIER;
    SELECT @EmployeeId = Id FROM @InsertedEmployee;

    -- Insert into EmployeeDetails and capture the Id
    INSERT INTO EmployeeDetails (EmployeeId, Address, PhoneNumber)
    OUTPUT INSERTED.Id INTO @InsertedEmployeeDetail
    VALUES (@EmployeeId, @Address, @PhoneNumber);

    -- Retrieve the EmployeeDetailId
    DECLARE @EmployeeDetailId UNIQUEIDENTIFIER;
    SELECT @EmployeeDetailId = Id FROM @InsertedEmployeeDetail;

    -- Insert into EmployeeBenefits
    INSERT INTO EmployeeBenefits (EmployeeDetailId, BenefitType, BenefitValue)
    VALUES (@EmployeeDetailId, @BenefitType, @BenefitValue);
END;