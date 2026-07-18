USE SchoolDB;
GO

CREATE TABLE Students
(
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    StudentNumber VARCHAR(20) NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    DateOfBirth DATE,
    Email VARCHAR(100),
    IsActive BIT DEFAULT 1
);
GO