CREATE PROCEDURE CreateUser
    @Username VARCHAR(50),
    @PasswordHash VARCHAR(256),
    @Email VARCHAR(100),
    @RoleName VARCHAR(50) = 'Customer',
    @AdminUserID INT
AS
BEGIN
    DECLARE @RoleID INT;
    DECLARE @UserID INT;

   
    SELECT @RoleID = RoleID
    FROM Roles
    WHERE RoleName = @RoleName;

    IF @RoleID IS NULL
    BEGIN
        RAISERROR('Role not found!', 16, 1);
        RETURN;
    END

   
    INSERT INTO Users (Username, PasswordHash, RoleID, Email)
    VALUES (@Username, @PasswordHash, @RoleID, @Email);

   
    SELECT @UserID = SCOPE_IDENTITY();

   
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (@AdminUserID, 'User Created', CONCAT('Username: ', @Username, ' Role: ', @RoleName));

   
    SELECT @UserID AS UserID;
END;
GO




CREATE PROCEDURE CreateUtility
    @UtilityName VARCHAR(100),
    @UtilityCode VARCHAR(10),
    @AdminUserID INT
AS
BEGIN
    DECLARE @UtilityID INT;

    -- Step 1: Check if the UtilityCode already exists in the system
    IF EXISTS (SELECT 1 FROM Utilities WHERE UtilityCode = @UtilityCode)
    BEGIN
        RAISERROR('UtilityCode already exists!', 16, 1);
        RETURN;
    END

   
    INSERT INTO Utilities (UtilityName, UtilityCode, CreatedBy, CreatedAt)
    VALUES (@UtilityName, @UtilityCode, @AdminUserID, GETDATE());

   
    SELECT @UtilityID = SCOPE_IDENTITY();

   
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (@AdminUserID, 'Utility Created', CONCAT('UtilityName: ', @UtilityName, ' UtilityCode: ', @UtilityCode));

   
    SELECT @UtilityID AS UtilityID;
END;
GO







CREATE PROCEDURE CreateCustomer
    @ReferenceNumber VARCHAR(50),
    @CustomerName VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(20),
    @UtilityCode VARCHAR(10),
    @AdminUserID INT
AS
BEGIN
    DECLARE @UtilityID INT;
    DECLARE @CustomerID INT;

   
    SELECT @UtilityID = UtilityID
    FROM Utilities
    WHERE UtilityCode = @UtilityCode;

    IF @UtilityID IS NULL
    BEGIN
        RAISERROR('Utility not found!', 16, 1);
        RETURN;
    END

   
    INSERT INTO Customers (ReferenceNumber, CustomerName, Email, Phone, UtilityID )
    VALUES (@ReferenceNumber, @CustomerName, @Email, @Phone, @UtilityID );


    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (@AdminUserID, 'Customer Created', CONCAT('ReferenceNumber: ', @ReferenceNumber, ' UtilityCode: ', @UtilityCode));

   
    SELECT SCOPE_IDENTITY() AS CustomerID;
END;
GO






CREATE PROCEDURE InitiatePayment
    @VendorCode VARCHAR(20),
    @CustomerReference VARCHAR(50),
    @Amount DECIMAL(18,2),
    @VendorUserID INT
AS
BEGIN
    DECLARE @VendorID INT;
    DECLARE @UtilityID INT;
    DECLARE @CustomerID INT;
    DECLARE @TransactionID UNIQUEIDENTIFIER;

 
    SELECT @VendorID = VendorID
    FROM Vendors
    WHERE VendorCode = @VendorCode;

    IF @VendorID IS NULL
    BEGIN
        RAISERROR('Vendor not found!', 16, 1);
        RETURN;
    END

   
    SELECT @CustomerID = CustomerID
    FROM Customers
    WHERE ReferenceNumber = @CustomerReference;

    IF @CustomerID IS NULL
    BEGIN
        RAISERROR('Customer not found!', 16, 1);
        RETURN;
    END

   
    SET @TransactionID = NEWID();
    INSERT INTO ReceivedTransactions (TransactionID, VendorID, CustomerID, UtilityID, ReferenceNumber, Amount, Status)
    VALUES (@TransactionID, @VendorID, @CustomerID, @UtilityID, @CustomerReference, @Amount, 'Pending');

   
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (@VendorUserID, 'Payment Initiated', CONCAT('TransactionID: ', @TransactionID, ' Amount: ', @Amount));

   
    SELECT @TransactionID AS TransactionID;
END;
GO






CREATE PROCEDURE ProcessTransaction
    @TransactionID UNIQUEIDENTIFIER,
    @AdminUserID INT
AS
BEGIN
    DECLARE @Status VARCHAR(20);
    DECLARE @UtilityToken VARCHAR(100);
    DECLARE @UtilityReceiptNo VARCHAR(50);

    SELECT @Status = Status
    FROM ReceivedTransactions
    WHERE TransactionID = @TransactionID;

    IF @Status = 'Success' OR @Status = 'Failed'
    BEGIN
        RAISERROR('Transaction already processed!', 16, 1);
        RETURN;
    END

   
    UPDATE ReceivedTransactions
    SET Status = 'Success', UtilityToken = 'SimulatedToken123', UtilityReceiptNo = 'Receipt123', ProcessedAt = GETDATE()
    WHERE TransactionID = @TransactionID;

   
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (@AdminUserID, 'Transaction Processed', CONCAT('TransactionID: ', @TransactionID, ' Status: Success'));

   
    SELECT ' Successful' AS Message;
END;
GO











CREATE PROCEDURE DeductVendorBalance
    @TransactionID UNIQUEIDENTIFIER,
    @Amount DECIMAL(18,2)
AS
BEGIN
    DECLARE @VendorID INT;
    DECLARE @CurrentBalance DECIMAL(18,2);

   
    SELECT @VendorID = VendorID, @CurrentBalance = Balance
    FROM Vendors
    WHERE VendorID IN (SELECT VendorID FROM ReceivedTransactions WHERE TransactionID = @TransactionID);

   
    IF @VendorID IS NULL
    BEGIN
        RAISERROR('Vendor not found for this transaction.', 16, 1);
        RETURN;
    END

    IF @CurrentBalance < @Amount
    BEGIN
        RAISERROR('Vendor does not have enough balance to complete this transaction.', 16, 1);
        RETURN;
    END

   
    UPDATE Vendors
    SET Balance = Balance - @Amount
    WHERE VendorID = @VendorID;

   
    UPDATE ReceivedTransactions
    SET SentToMomo = 1, MomoRequestTime = GETDATE()
    WHERE TransactionID = @TransactionID;

   
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (NULL, 'Vendor Balance Deducted', CONCAT('VendorID: ', @VendorID, ' Amount: ', @Amount));

   
    SELECT 'Successful' AS Status;

END;
GO
