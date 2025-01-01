CREATE OR ALTER PROCEDURE InsertEmployeeData
    @Name NVARCHAR(MAX),
    @Position NVARCHAR(MAX),
    @Office NVARCHAR(MAX),
    @Age INT,
    @Salary INT,
    @Address NVARCHAR(MAX),
    @PhoneNumber NVARCHAR(MAX),
    @BenefitType NVARCHAR(MAX),
    @BenefitValue INT,
    @EmployeeId UNIQUEIDENTIFIER OUTPUT -- Output parameter for EmployeeId
AS
BEGIN
    SET NOCOUNT ON;

    -- Begin the transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Table variable to capture inserted Employee Id
        DECLARE @InsertedEmployee TABLE (Id UNIQUEIDENTIFIER);

        -- Insert into Employees and capture the Id
        INSERT INTO dbo.Employees (Name, Position, Office, Age, Salary)
        OUTPUT INSERTED.Id INTO @InsertedEmployee
        VALUES (@Name, @Position, @Office, @Age, @Salary);

        -- Retrieve the EmployeeId
        SELECT @EmployeeId = Id FROM @InsertedEmployee;

        -- Insert into EmployeeDetails
        INSERT INTO EmployeeDetails (EmployeeId, Address, PhoneNumber)
        OUTPUT INSERTED.Id
        VALUES (@EmployeeId, @Address, @PhoneNumber);

        -- Insert into EmployeeBenefits
        INSERT INTO EmployeeBenefits (EmployeeDetailId, BenefitType, BenefitValue)
        VALUES (
            (SELECT Id FROM EmployeeDetails WHERE EmployeeId = @EmployeeId),
            @BenefitType,
            @BenefitValue
        );

        -- Commit the transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback the transaction on error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Rethrow the error
        THROW;
    END CATCH;
END;