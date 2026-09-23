-- SQL Script to create the Student database and required Registration table.

-- Create the Student database
IF DB_ID(N'Student') IS NULL CREATE DATABASE Student;
GO

USE Student;
GO

-- Registration table: stores student registration details
IF OBJECT_ID(N'dbo.Registration', N'U') IS NULL
CREATE TABLE Registration (
    regNo INT PRIMARY KEY,
    firstName VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    dateOfBirth DATETIME NOT NULL,
    gender VARCHAR(50) NOT NULL,
    address VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    mobilePhone INT NOT NULL,
    homePhone INT NOT NULL,
    parentName VARCHAR(50) NOT NULL,
    nic VARCHAR(50) NOT NULL,
    contactNo INT NOT NULL
);
GO
