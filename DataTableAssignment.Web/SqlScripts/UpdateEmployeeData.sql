CREATE PROCEDURE UpdateEmployeeData
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
        -- Update the Employees table
        UPDATE dbo.Employees
        SET Name = @Name,
            Position = @Position,
            Office = @Office,
            Age = @Age,
            Salary = @Salary
        WHERE Id = @EmployeeId;

        -- Check if the employee exists
        IF @@ROWCOUNT = 0
            THROW 50001, 'Employee not found.', 1;

        -- Update the EmployeeDetails table
        UPDATE dbo.EmployeeDetails
        SET Address = @Address,
            PhoneNumber = @PhoneNumber
        WHERE EmployeeId = @EmployeeId;

        -- Check if employee details exist
        IF @@ROWCOUNT = 0
            THROW 50002, 'Employee details not found.', 1;

        -- Update the EmployeeBenefits table
        UPDATE dbo.EmployeeBenefits
        SET BenefitType = @BenefitType,
            BenefitValue = @BenefitValue
        WHERE EmployeeDetailId = (SELECT Id FROM EmployeeDetails WHERE EmployeeId = @EmployeeId);

        -- Check if employee benefits exist
        IF @@ROWCOUNT = 0
            THROW 50003, 'Employee benefits not found.', 1;

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

    -- Return the EmployeeId to confirm the operation
    RETURN @EmployeeId;
END;
