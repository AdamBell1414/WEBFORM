CREATE PROCEDURE sp_CreateCustomer
    @ReferenceNumber VARCHAR(50),
    @CustomerName VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(20),
    @PasswordHash VARCHAR(256),
    @UtilityCode VARCHAR(10),
    @AdminUserID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UtilityID INT;
    DECLARE @CustomerID INT;
    DECLARE @UserID INT;
    DECLARE @RoleID INT;

    -- Get UtilityID
    SELECT @UtilityID = UtilityID FROM Utilities WHERE UtilityCode = @UtilityCode;

    IF @UtilityID IS NULL
    BEGIN
        RAISERROR('Utility not found!', 16, 1);
        RETURN;
    END

    -- Get RoleID for 'Customer'
    SELECT @RoleID = RoleID FROM Roles WHERE RoleName = 'Customer';

    IF @RoleID IS NULL
    BEGIN
        RAISERROR('Customer role does not exist!', 16, 1);
        RETURN;
    END

    -- Create User for Customer
    INSERT INTO Users (Username, PasswordHash, RoleID, Email)
    VALUES (@ReferenceNumber, @PasswordHash, @RoleID, @Email);

    SELECT @UserID = SCOPE_IDENTITY();

    -- Insert Customer
    INSERT INTO Customers (ReferenceNumber, CustomerName, Email, Phone, UtilityID)
    VALUES (@ReferenceNumber, @CustomerName, @Email, @Phone, @UtilityID);

    SELECT @CustomerID = SCOPE_IDENTITY();

    -- Log audit
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (@AdminUserID, 'Customer Created', CONCAT('ReferenceNumber: ', @ReferenceNumber, ', UserID: ', @UserID));

    -- Return created CustomerID and UserID
    SELECT @CustomerID AS CustomerID, @UserID AS UserID;
END;
Go

ALTER PROCEDURE sp_CreateVendor
    @VendorCode VARCHAR(20),
    @VendorName VARCHAR(100),
    @ContactEmail VARCHAR(100),
    @ContactPhone VARCHAR(20),
    @PasswordHash VARCHAR(256),
    @Balance DECIMAL(18,2),
    @AdminUserID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @VendorID INT;
    DECLARE @UserID INT;
    DECLARE @RoleID INT;

    -- Check for duplicate vendor code
    IF EXISTS (SELECT 1 FROM Vendors WHERE VendorCode = @VendorCode)
    BEGIN
        RAISERROR('VendorCode already exists!', 16, 1);
        RETURN;
    END

    -- Get RoleID for 'Vendor'
    SELECT @RoleID = RoleID FROM Roles WHERE RoleName = 'Vendor';

    IF @RoleID IS NULL
    BEGIN
        RAISERROR('Vendor role does not exist!', 16, 1);
        RETURN;
    END

    -- Create user account for vendor
    INSERT INTO Users (Username, PasswordHash, RoleID, Email)
    VALUES (@VendorCode, @PasswordHash, @RoleID, @ContactEmail);

    SELECT @UserID = SCOPE_IDENTITY();

    -- Create vendor, correctly assigning CreatedBy as the AdminUserID
    INSERT INTO Vendors (
        VendorCode, VendorName, ContactEmail, ContactPhone, Balance, CreatedBy, CreatedAt
    )
    VALUES (
        @VendorCode, @VendorName, @ContactEmail, @ContactPhone, @Balance, @AdminUserID, GETDATE()
    );

    SET @VendorID = SCOPE_IDENTITY();

    -- Log audit
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (@AdminUserID, 'Vendor Created', CONCAT('VendorCode: ', @VendorCode, ', Created UserID: ', @UserID));

    -- Return vendor ID and user ID
    SELECT @VendorID AS VendorID, @UserID AS UserID;
END;
GO

