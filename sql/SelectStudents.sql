USE SchoolDB;
GO

SELECT *
FROM Students;

/*  Literals */

SELECT
'Welcome to MDB622' AS Message,
123 AS Number,
GETDATE() AS Today;

/*  Functions */ 

SELECT
FirstName,
LastName,
ISNULL(Email, 'No Email') AS EmailAddress,
GETDATE() AS CurrentDate
FROM Students;

/*  Aggregate Functions */ 

SELECT COUNT(*) AS TotalStudents
FROM Students;


SELECT MIN(DateOfBirth) AS OldestStudent
FROM Students;

SELECT MAX(DateOfBirth) AS YoungestStudent
FROM Students;

SELECT COUNT(*) AS ActiveStudents
FROM Students
WHERE IsActive = 1;