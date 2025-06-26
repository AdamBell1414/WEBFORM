CREATE PROCEDURE InsertDefaultRoles
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert default roles if they don't already exist
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'SuperAdmin')
        INSERT INTO Roles (RoleName) VALUES ('SuperAdmin');

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'Admin')
        INSERT INTO Roles (RoleName) VALUES ('Admin');

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'Vendor')
        INSERT INTO Roles (RoleName) VALUES ('Vendor');

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'Customer')
        INSERT INTO Roles (RoleName) VALUES ('Customer');

    -- Return the inserted (or existing) roles
    SELECT * FROM Roles;
END;
GO


CREATE PROCEDURE CreateSuperAdmin
    @Username VARCHAR(50),
    @PasswordHash VARCHAR(256),
    @Email VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RoleID INT;
    DECLARE @UserID INT;

    -- Fetch RoleID for SuperAdmin
    SELECT @RoleID = RoleID FROM Roles WHERE RoleName = 'SuperAdmin';

    IF @RoleID IS NULL
    BEGIN
        RAISERROR('SuperAdmin role not found. Run InsertDefaultRoles first.', 16, 1);
        RETURN;
    END

    -- Check if a SuperAdmin already exists
    IF EXISTS (
        SELECT 1 FROM Users U
        INNER JOIN Roles R ON U.RoleID = R.RoleID
        WHERE R.RoleName = 'SuperAdmin'
    )
    BEGIN
        RAISERROR('A SuperAdmin already exists.', 16, 1);
        RETURN;
    END

    -- Create SuperAdmin user
    INSERT INTO Users (Username, PasswordHash, RoleID, Email)
    VALUES (@Username, @PasswordHash, @RoleID, @Email);

    SET @UserID = SCOPE_IDENTITY();

    -- Log the creation with UserID = NULL (system action)
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (NULL, 'SuperAdmin Created', CONCAT('Username: ', @Username));

    SELECT @UserID AS SuperAdminUserID;
END;
GO


-- Step 1: Insert all default roles
EXEC InsertDefaultRoles;

-- Step 2: Create SuperAdmin user (only once!)
EXEC CreateSuperAdmin
    @Username = 'superadmin',
    @PasswordHash = 'Godsplan',  
    @Email = 'superadmin@example.com';



select * from Customers;

select * from Users;
SELECT * FROM Utilities
select * from Vendors;

select * from   Customers
select * from Roles

SELECT * FROM AuditLogs
ORDER BY Timestamp DESC

CREATE PROCEDURE CreateVendor
    @VendorCode VARCHAR(20),
    @VendorName VARCHAR(100),
    @ContactEmail VARCHAR(100),
    @ContactPhone VARCHAR(20),
    @AdminUserID INT
AS
BEGIN
    DECLARE @VendorID INT;

    -- Check if VendorCode already exists
    IF EXISTS (SELECT 1 FROM Vendors WHERE VendorCode = @VendorCode)
    BEGIN
        RAISERROR('VendorCode already exists!', 16, 1);
        RETURN;
    END

    -- Insert new vendor
    INSERT INTO Vendors (VendorCode, VendorName, ContactEmail, ContactPhone, CreatedBy, CreatedAt)
    VALUES (@VendorCode, @VendorName, @ContactEmail, @ContactPhone, @AdminUserID, GETDATE());

    -- Get the new VendorID
    SELECT @VendorID = SCOPE_IDENTITY();

    -- Audit log
    INSERT INTO AuditLogs (UserID, Action, Details)
    VALUES (@AdminUserID, 'Vendor Created', CONCAT('VendorCode: ', @VendorCode, ', VendorName: ', @VendorName));

    -- Return new VendorID
    SELECT @VendorID AS VendorID;
END;
GO



CREATE TABLE LoginAttempts (
    AttemptID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    AttemptTime DATETIME DEFAULT GETDATE(),
    Success BIT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
Go

CREATE PROCEDURE sp_LoginUser
    @UsernameOrEmail VARCHAR(100),
    @PasswordHash VARCHAR(256)
AS
BEGIN
    DECLARE @UserID INT, @IsActive BIT, @AttemptCount INT;

    SELECT 
        @UserID = UserID,
        @IsActive = IsActive
    FROM Users 
    WHERE (Username = @UsernameOrEmail OR Email = @UsernameOrEmail)
      AND PasswordHash = @PasswordHash;

    IF @UserID IS NOT NULL
    BEGIN
        -- Check account status
        IF @IsActive = 0
        BEGIN
            SELECT -2 AS ResultCode; -- Account locked
            RETURN;
        END

        -- Reset previous failed attempts on successful login
        DELETE FROM LoginAttempts WHERE UserID = @UserID AND Success = 0;

        SELECT u.UserID, u.Username, u.Email, r.RoleName, u.RoleID
        FROM Users u
        INNER JOIN Roles r ON u.RoleID = r.RoleID
        WHERE u.UserID = @UserID;
    END
    ELSE
    BEGIN
        -- Track failed login attempt
        SELECT @UserID = UserID FROM Users 
        WHERE (Username = @UsernameOrEmail OR Email = @UsernameOrEmail);

        IF @UserID IS NOT NULL
        BEGIN
            INSERT INTO LoginAttempts (UserID, Success) VALUES (@UserID, 0);

            -- Lock account if 3 failed attempts
            SELECT @AttemptCount = COUNT(*) 
            FROM LoginAttempts 
            WHERE UserID = @UserID AND Success = 0;

            IF @AttemptCount >= 3
            BEGIN
                UPDATE Users SET IsActive = 0 WHERE UserID = @UserID;
                SELECT -2 AS ResultCode; -- Account locked
                RETURN;
            END
        END

        SELECT -1 AS ResultCode; -- Invalid login
    END
END
