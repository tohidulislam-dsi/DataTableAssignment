CREATE OR ALTER PROCEDURE UpdateEmployeeData
    @EmployeeId UNIQUEIDENTIFIER,
    @Name NVARCHAR(MAX),
    @Position NVARCHAR(MAX),
    @Office NVARCHAR(MAX),
    @Age INT,
    @Salary INT,
    @Address NVARCHAR(MAX) = NULL,
    @PhoneNumber NVARCHAR(MAX) = NULL,
    @EmployeeBenefits UpdateEmployeeBenefitType READONLY -- Table-valued parameter for EmployeeBenefits
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

        -- Update EmployeeDetails table if Address or PhoneNumber is provided
        IF @Address IS NOT NULL OR @PhoneNumber IS NOT NULL
        BEGIN
            DECLARE @EmployeeDetailIdTable TABLE (Id UNIQUEIDENTIFIER);

            -- Check if EmployeeDetails exists
            IF EXISTS (SELECT 1 FROM EmployeeDetails WHERE EmployeeId = @EmployeeId)
            BEGIN
                -- Update existing EmployeeDetails
                UPDATE EmployeeDetails
                SET
                    Address = ISNULL(@Address, Address),
                    PhoneNumber = ISNULL(@PhoneNumber, PhoneNumber)
                WHERE EmployeeId = @EmployeeId;

                -- Retrieve the EmployeeDetailId
                INSERT INTO @EmployeeDetailIdTable (Id)
                SELECT Id FROM EmployeeDetails WHERE EmployeeId = @EmployeeId;
            END
            ELSE
            BEGIN
                -- Insert new EmployeeDetails and capture the new EmployeeDetailId
                INSERT INTO EmployeeDetails (EmployeeId, Address, PhoneNumber)
                OUTPUT INSERTED.Id INTO @EmployeeDetailIdTable
                VALUES (@EmployeeId, @Address, @PhoneNumber);
            END

            -- Get the EmployeeDetailId from the table variable
            DECLARE @EmployeeDetailId UNIQUEIDENTIFIER;
            SELECT @EmployeeDetailId = Id FROM @EmployeeDetailIdTable;

            -- Merge EmployeeBenefits if any benefits are provided
            IF EXISTS (SELECT 1 FROM @EmployeeBenefits)
            BEGIN
                MERGE EmployeeBenefits AS target
                USING @EmployeeBenefits AS source
                ON target.EmployeeDetailId = @EmployeeDetailId AND target.Id = source.BenefitId
                WHEN MATCHED THEN
                    UPDATE SET
                        BenefitType = source.BenefitType,
                        BenefitValue = source.BenefitValue
                WHEN NOT MATCHED BY TARGET THEN
                    INSERT (EmployeeDetailId, BenefitType, BenefitValue)
                    VALUES (@EmployeeDetailId, source.BenefitType, source.BenefitValue);
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
