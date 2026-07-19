USE SchoolDB

GO

/*
Epic: Student Management System

Feature: Student Reporting and Automation

User Story

As a Principal, I want the system to automatically calculate information such as 
the total number of students and display student names in a consistent format 
so that reports are accurate, reusable, and easy to generate.

*/


-- Example 1 – Total Number of Students
CREATE FUNCTION fnTotalStudents()

RETURNS INT

AS
BEGIN

    DECLARE @Total INT;

    SELECT @Total = COUNT(*)
    FROM Students;

    RETURN @Total;

END;
GO

-- Check if the function was created 
SELECT name
FROM sys.objects
WHERE type = 'FN';

-- Execute the Function

SELECT dbo.fnTotalStudents() AS TotalStudents;


-- Example: Full-name function

CREATE FUNCTION dbo.fnFullName
(
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50)
)
RETURNS VARCHAR(120)
AS
BEGIN
    RETURN @FirstName + ' ' + @LastName;
END;
GO

-- Run fnFullName
SELECT
    StudentNumber,
    dbo.fnFullName(FirstName, LastName) AS FullName
FROM Students;


-- Calculate Student Age
CREATE FUNCTION fnCalculateAge
(
    @DateOfBirth DATE
)

RETURNS INT

AS
BEGIN

    RETURN DATEDIFF(YEAR, @DateOfBirth, GETDATE());

END;
GO


-- Execute Calculate Student Age
SELECT
    FirstName,
    LastName,
    dbo.fnCalculateAge(DateOfBirth) AS Age
FROM Students;


/*

User Story

Epic: Student Reporting

Feature: Course Enrolment Reports

User Story

As a Registrar, I want to display all students enrolled in a specific course so 
that I can quickly generate course attendance lists.

*/

-- Table-valued function

USE SchoolDB;
GO

CREATE FUNCTION dbo.fnStudentsByCourse
(
    @CourseID INT
)

RETURNS TABLE

AS

RETURN
(
    SELECT
        S.StudentNumber,
        S.FirstName,
        S.LastName,
        C.CourseCode,
        C.CourseName
    FROM Students AS S
    INNER JOIN Enrollments AS E
        ON S.StudentID = E.StudentID
    INNER JOIN Courses AS C
        ON E.CourseID = C.CourseID
    WHERE C.CourseID = @CourseID
);
GO


-- Execute the table-valued Function
SELECT *
FROM dbo.fnStudentsByCourse(2);

/*
Take note : Unlike a Scalar Function that returns one value, 
a Table-Valued Function returns a complete table. */


--  Example 2: Aggregate Function (Average Student Age)

/*
User Story

Epic: Student Statistics

Feature: Student Demographics

User Story

As a Principal, I want to know the average age of students 
so that I can better understand the student population.*/

-- Example :  Create Average Function

CREATE FUNCTION dbo.fnAverageStudentAge()

RETURNS DECIMAL(5,2)

AS
BEGIN

    DECLARE @AverageAge DECIMAL(5,2);

    SELECT @AverageAge =
        AVG(DATEDIFF(YEAR, DateOfBirth, GETDATE()) * 1.0)
    FROM Students;

    RETURN @AverageAge;

END;
GO

-- Test or run the Average function:
SELECT dbo.fnAverageStudentAge() AS AverageAge;


-- Another Aggregate Function (Active Students)

/* 
User Story

As a Registrar, I want to know how many students are currently active 
so that I can prepare registration reports. */

-- Create Aggregate Functions:
CREATE FUNCTION dbo.fnActiveStudents()

RETURNS INT

AS
BEGIN

    DECLARE @Total INT;

    SELECT @Total = COUNT(*)
    FROM Students
    WHERE IsActive = 1;

    RETURN @Total;

END;
GO


-- Test or run Aggregate Functions:
SELECT dbo.fnActiveStudents() AS ActiveStudents;