USE SchoolDB

GO


/* Uppercase */

SELECT
UPPER(FirstName)
FROM Students;


/* Lowercase */
SELECT
LOWER(FirstName)
FROM Students;

/* length */

SELECT
LEN(FirstName)
FROM Students;

/*Today's Date Default */
SELECT
GETDATE();

/* DD/MM/YYYY */
SELECT FORMAT(GETDATE(), 'dd/MM/yyyy') AS Today;


/* MM/DD/YYYY */
SELECT FORMAT(GETDATE(), 'MM/dd/yyyy') AS Today;

/* YYYY-MM-DD */
SELECT FORMAT(GETDATE(), 'yyyy-MM-dd') AS Today;

/* Day, Month, Year */
SELECT FORMAT(GETDATE(), 'dddd, dd MMMM yyyy') AS Today;

/* Date and Time */
SELECT FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm:ss') AS CurrentDateTime;



/* NULL Handling */

SELECT
FirstName,
ISNULL(Email,'No Email')
FROM Students;

/* COUNT */
SELECT
COUNT(*)
AS TotalStudents
FROM Students;

/* Min */
SELECT
MIN(DateOfBirth)
AS OldestStudent
FROM Students;

/* Max */
SELECT
MAX(DateOfBirth)
AS YoungestStudent
FROM Students;

/* Display student's name as well */
SELECT
    FirstName,
    LastName,
    DateOfBirth
FROM Students
WHERE DateOfBirth =
(
    SELECT MIN(DateOfBirth)
    FROM Students
);

/* Youngest student Max */
SELECT
    FirstName,
    LastName,
    DateOfBirth
FROM Students
WHERE DateOfBirth =
(
    SELECT MAX(DateOfBirth)
    FROM Students
);

/* Using CONVERT() 
This is the traditional SQL Server method.
*/

/*DD/MM/YYYY */
SELECT CONVERT(VARCHAR(10), GETDATE(), 103) AS Today;

/* YYYY-MM-DD */
SELECT CONVERT(VARCHAR(10), GETDATE(), 23) AS Today;

/* DD Mon YYYY */
SELECT CONVERT(VARCHAR(11), GETDATE(), 106) AS Today;

/* Date and Time */

SELECT CONVERT(VARCHAR(20), GETDATE(), 120) AS DateTime;


/*
MIN() returns the earliest (smallest) date, which represents the oldest person. 
MAX() returns the latest (largest) date, which represents the youngest person. 
This is because dates are stored chronologically, allowing SQL Server to compare them just like numbers.
*/