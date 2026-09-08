-- SQL Script to create the Student database, Logins and Registration tables,
-- and seed the default admin login.

-- Create the Student database
CREATE DATABASE Student;
GO

USE Student;
GO

-- Logins table: stores valid application users
CREATE TABLE Logins (
    LoginID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL
);
GO

-- Seed the default user
INSERT INTO Logins (Username, Password) VALUES ('Admin', 'Campusone@123');
GO

-- Registration table: stores student registration details
CREATE TABLE Registration (
    regNo INT PRIMARY KEY,
    firstName VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    dateOfBirth DATE NOT NULL,
    gender VARCHAR(10) NOT NULL,
    address VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL,
    mobilePhone VARCHAR(15) NOT NULL,
    homePhone VARCHAR(15),
    parentName VARCHAR(100) NOT NULL,
    NIC VARCHAR(20) NOT NULL,
    contactNo VARCHAR(15) NOT NULL
);
GO
