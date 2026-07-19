USE SchoolDB

GO

/* Transactions allow us to treat several SQL statements as one unit of work. */

BEGIN TRANSACTION;

INSERT INTO Students
(
StudentNumber,
FirstName,
LastName,
DateOfBirth,
Email,
IsActive
)

VALUES
(
'ST1006',
'Aruna',
'Lusukama',
'2004-10-20',
'aruna.l@ctucareer.co.za',
1
);

/* display student */

SELECT * FROM Students

/* Rollback class (undo) */

ROLLBACK;

/* Display students */

SELECT * FROM Students

/*
Note : Rollback undo changes commit 
*/