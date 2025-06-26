CREATE PROCEDURE sp_ValidateUtilityReference
    @VendorCode VARCHAR(20),
    @ReferenceNumber VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @VendorID INT;

    SELECT @VendorID = VendorID
    FROM Vendors
    WHERE VendorCode = @VendorCode;

    IF @VendorID IS NULL
    BEGIN
        RAISERROR('Vendor not found', 16, 1);
        RETURN;
    END

    -- Validate Customer
    SELECT TOP 1 ReferenceNumber, CustomerName, Email, Phone, UtilityID
    FROM Customers
    WHERE ReferenceNumber = @ReferenceNumber;
END
Go

CREATE PROCEDURE InitiateVendorPaymentTransaction
    @VendorCode VARCHAR(20),
    @CustomerReference VARCHAR(50),
    @UtilityCode VARCHAR(20),
    @Amount DECIMAL(18,2),
    @VendorUserID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @VendorID INT, @CustomerID INT, @UtilityID INT;
    DECLARE @TransactionID UNIQUEIDENTIFIER = NEWID();
    DECLARE @CurrentBalance DECIMAL(18,2);

    -- 1. Get Vendor ID and Balance
    SELECT @VendorID = VendorID, @CurrentBalance = Balance
    FROM Vendors
    WHERE VendorCode = @VendorCode;

    IF @VendorID IS NULL
    BEGIN
        RAISERROR('Vendor not found.', 16, 1);
        RETURN;
    END

    -- 2. Check if vendor has enough balance
    IF @CurrentBalance < @Amount
    BEGIN
        RAISERROR('Insufficient vendor balance.', 16, 1);
        RETURN;
    END

    -- 3. Get CustomerID from Reference
    SELECT @CustomerID = CustomerID
    FROM Customers
    WHERE ReferenceNumber = @CustomerReference;

    IF @CustomerID IS NULL
    BEGIN
        RAISERROR('Customer not found.', 16, 1);
        RETURN;
    END

    -- 4. Get UtilityID from UtilityCode
    SELECT @UtilityID = UtilityID
    FROM Utilities
    WHERE UtilityCode = @UtilityCode;

    IF @UtilityID IS NULL
    BEGIN
        RAISERROR('Utility not found.', 16, 1);
        RETURN;
    END

    -- 5. Deduct balance
    UPDATE Vendors
    SET Balance = Balance - @Amount
    WHERE VendorID = @VendorID;

    -- 6. Insert transaction as pending with SentToMomo = 1
    INSERT INTO ReceivedTransactions (
        TransactionID, VendorID, CustomerID, UtilityID,
        ReferenceNumber, Amount, Status, SentToMomo, MomoRequestTime
    ) VALUES (
        @TransactionID, @VendorID, @CustomerID, @UtilityID,
        @CustomerReference, @Amount, 'Pending', 1, GETDATE()
    );

    -- 7. Audit log
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (
        @VendorUserID, 
        'Initiated Payment',
        CONCAT('TransactionID: ', @TransactionID, ', Amount: ', @Amount)
    );

    -- 8. Return transaction ID
    SELECT @TransactionID AS TransactionID;
END;
GO
