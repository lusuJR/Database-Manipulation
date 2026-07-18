/* Retrieve all students */

SELECT *
FROM Students;

/* Retrieve only name */

SELECT
FirstName,
LastName
FROM Students;


/* Get Alias */
SELECT
FirstName AS Name,
LastName AS Surname
FROM Students;

/* Expression */
SELECT
FirstName + ' ' + LastName AS FullName
FROM Students;

/* Literals */
SELECT
'ABC College' AS College,
GETDATE() AS Today;

/* Where */
SELECT *
FROM Students
WHERE IsActive = 1;

/* Example return Selemami */
SELECT *
FROM Students
WHERE LastName='Selemani';

/* Like  */

SELECT *
FROM Students
WHERE FirstName LIKE 'J%';


/* Null */

SELECT *
FROM Students
WHERE Email IS NULL;

/* Order by */

SELECT *
FROM Students
ORDER BY LastName;


/* Descending */

SELECT *
FROM Students
ORDER BY DateOfBirth DESC;


/* INNER JOIN  for example : The Principal wants to know which course every student is taking.*/

SELECT
S.StudentNumber,
S.FirstName,
S.LastName,
C.CourseName
FROM Students S
INNER JOIN Enrollments E
    ON S.StudentID = E.StudentID
INNER JOIN Courses C
    ON E.CourseID = C.CourseID;


/* Example of : LEFT JOIN : */
SELECT
S.FirstName,
C.CourseName
FROM Students S
LEFT JOIN Enrollments E
ON S.StudentID=E.StudentID
LEFT JOIN Courses C
ON E.CourseID=C.CourseID;

/* COUNT */
SELECT
COUNT(*) AS TotalStudents
FROM Students;


/* GROUP BY */

SELECT
CourseID,
COUNT(*) AS TotalStudents
FROM Enrollments
GROUP BY CourseID;



