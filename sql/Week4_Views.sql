USE SchoolDB

GO

-- First view for activity students
CREATE VIEW vwActiveStudents
AS
SELECT
    StudentID,
    StudentNumber,
    FirstName,
    LastName,
    Email
FROM Students
WHERE IsActive = 1;
GO

-- Use the View
SELECT *
FROM vwActiveStudents;

/* 
Take note: A view is a saved SQL query. 
It behaves like a virtual table, but it does not normally store its own data.
*/

-- Example 2: Create a view using joins

CREATE VIEW vwStudentCourses
AS
SELECT
    S.StudentNumber,
    S.FirstName,
    S.LastName,
    C.CourseCode,
    C.CourseName,
    E.EnrollmentDate
FROM Students AS S
INNER JOIN Enrollments AS E
    ON S.StudentID = E.StudentID
INNER JOIN Courses AS C
    ON E.CourseID = C.CourseID;
GO

-- Test it

SELECT *
FROM vwStudentCourses;


-- Filter information from a view

SELECT *
FROM vwStudentCourses
WHERE CourseCode = 'DB101';


SELECT *
FROM vwStudentCourses
ORDER BY FirstName;

/* Take note: A view can be queried using WHERE, 
ORDER BY and other normal SELECT features. */


/* Modify a view 
User story : I want to add a email to the report
*/


ALTER VIEW vwStudentCourses
AS
SELECT
    S.StudentNumber,
    S.FirstName,
    S.LastName,
    S.Email,
    C.CourseCode,
    C.CourseName,
    E.EnrollmentDate
FROM Students AS S
INNER JOIN Enrollments AS E
    ON S.StudentID = E.StudentID
INNER JOIN Courses AS C
    ON E.CourseID = C.CourseID;
GO

-- Test it

SELECT *
FROM vwStudentCourses;

/* User story : Create a view that hides the student date of birth:*/
-- Security example
CREATE VIEW vwStudentContactDetails
AS
SELECT
    StudentNumber,
    FirstName,
    LastName,
    Email
FROM Students;
GO

-- Test 

SELECT *
FROM vwStudentContactDetails;

-- Drop a view demonstration
DROP VIEW vwStudentContactDetails;
GO



/* 
Class activity:

vwCourseEnrollmentSummary

It should display:

Course code
Course name
Number of enrolled students

*/





CREATE VIEW vwCourseEnrollmentSummary
AS
SELECT
    C.CourseCode,
    C.CourseName,
    COUNT(E.EnrollmentID) AS TotalEnrollments
FROM Courses AS C
LEFT JOIN Enrollments AS E
    ON C.CourseID = E.CourseID
GROUP BY
    C.CourseCode,
    C.CourseName;
GO

SELECT *
FROM vwCourseEnrollmentSummary;