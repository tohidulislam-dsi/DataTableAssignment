CREATE OR ALTER PROCEDURE UpdateEmployeeData
    @EmployeeId UNIQUEIDENTIFIER,
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

    -- Begin the transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Update Employees table
        UPDATE dbo.Employees
        SET
            Name = @Name,
            Position = @Position,
            Office = @Office,
            Age = @Age,
            Salary = @Salary
        WHERE Id = @EmployeeId;


        -- Update EmployeeDetails table
        UPDATE dbo.EmployeeDetails
        SET
            Address = @Address,
            PhoneNumber = @PhoneNumber
        WHERE EmployeeId = @EmployeeId;

        -- Update EmployeeBenefits table using join for efficiency
        UPDATE eb
        SET
            eb.BenefitType = @BenefitType,
            eb.BenefitValue = @BenefitValue
        FROM dbo.EmployeeBenefits eb
        JOIN dbo.EmployeeDetails ed ON eb.EmployeeDetailId = ed.Id
        WHERE ed.EmployeeId = @EmployeeId;

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