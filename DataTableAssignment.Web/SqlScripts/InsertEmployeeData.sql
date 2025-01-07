CREATE OR ALTER PROCEDURE InsertEmployeeData
    @Name NVARCHAR(MAX),
    @Position NVARCHAR(MAX),
    @Office NVARCHAR(MAX),
    @Age INT,
    @Salary INT,
    @Address NVARCHAR(MAX) = NULL,
    @PhoneNumber NVARCHAR(MAX) = NULL,
    @BenefitType NVARCHAR(MAX) = NULL,
    @BenefitValue INT = NULL,
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

        -- Insert into EmployeeDetails if Address or PhoneNumber is provided
        IF @Address IS NOT NULL OR @PhoneNumber IS NOT NULL
        BEGIN
            DECLARE @InsertedEmployeeDetail TABLE (Id UNIQUEIDENTIFIER);

            INSERT INTO EmployeeDetails (EmployeeId, Address, PhoneNumber)
            OUTPUT INSERTED.Id INTO @InsertedEmployeeDetail
            VALUES (@EmployeeId, @Address, @PhoneNumber);

            -- Retrieve the EmployeeDetailId
            DECLARE @EmployeeDetailId UNIQUEIDENTIFIER;
            SELECT @EmployeeDetailId = Id FROM @InsertedEmployeeDetail;

            -- Insert into EmployeeBenefits if BenefitType or BenefitValue is provided
            IF @BenefitType IS NOT NULL OR @BenefitValue IS NOT NULL
            BEGIN
                INSERT INTO EmployeeBenefits (EmployeeDetailId, BenefitType, BenefitValue)
                VALUES (@EmployeeDetailId, @BenefitType, @BenefitValue);
            END
        END

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
