CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(256) NOT NULL,
    RoleID INT,
    Email VARCHAR(100),
    IsActive BIT DEFAULT 1,  
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)  

);

GO
CREATE TABLE Vendors (
    VendorID INT IDENTITY(1,1) PRIMARY KEY,
    VendorCode VARCHAR(20) NOT NULL UNIQUE,
    VendorName VARCHAR(100) NOT NULL,
    Balance DECIMAL(18,2) DEFAULT 0,
    CreatedBy INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);
GO

ALTER TABLE Vendors
ADD 
    ContactEmail VARCHAR(100),
    ContactPhone VARCHAR(20);
GO


CREATE TABLE Utilities (
    UtilityID INT IDENTITY(1,1) PRIMARY KEY,
    UtilityName VARCHAR(100) NOT NULL,
    UtilityCode VARCHAR(10) NOT NULL,  
    CreatedBy INT,  
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)  
);
GO

CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    ReferenceNumber VARCHAR(50) NOT NULL UNIQUE,
    CustomerName VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Phone VARCHAR(20),
    UtilityID INT,
    FOREIGN KEY (UtilityID) REFERENCES Utilities(UtilityID)  
);
GO

CREATE TABLE ReceivedTransactions (
    TransactionID UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    VendorID INT,  
    CustomerID INT,
    UtilityID INT,
    ReferenceNumber VARCHAR(50) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,  
    Status VARCHAR(20) DEFAULT 'Pending',
    SentToMomo BIT DEFAULT 0,  
    MomoRequestTime DATETIME,
    UtilityToken VARCHAR(100),
    UtilityReceiptNo VARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE(),
    ProcessedAt DATETIME,
    FOREIGN KEY (VendorID) REFERENCES Vendors(VendorID),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (UtilityID) REFERENCES Utilities(UtilityID)
);
GO

CREATE TABLE MobileMoneyTransactions (
    MoMoID INT IDENTITY(1,1) PRIMARY KEY,
    TransactionID UNIQUEIDENTIFIER,
    VendorID INT,  
    Amount DECIMAL(18,2) NOT NULL,  
    Status VARCHAR(20) DEFAULT 'Pending',  
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TransactionID) REFERENCES ReceivedTransactions(TransactionID),
    FOREIGN KEY (VendorID) REFERENCES Vendors(VendorID)
);
GO
CREATE TABLE AuditLogs (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,  
    Action VARCHAR(100) NOT NULL,  
    Timestamp DATETIME DEFAULT GETDATE(),
    Details TEXT,  
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO
