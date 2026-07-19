USE SchoolDB

GO


/*

User Story

Epic: Student Management System

Feature: Protect Student Records

User Story

As a Database Administrator, I want the database to automatically 
protect important student records 
so that users cannot accidentally perform actions 
that violate the college's business rules.*/

/*
A Trigger is a special type of Stored Procedure that runs automatically 
whenever an INSERT, UPDATE or DELETE occurs.*/


--Example: Practical 1 – AFTER INSERT Trigger

/*
User Story

As a Registrar, I want the database to notify me whenever a new student is registered 
so that I know the registration was successful.*/

CREATE TRIGGER trgStudentInserted

ON Students

AFTER INSERT

AS
BEGIN

    PRINT 'New student successfully registered.';

END;
GO


-- Test the Trigger;

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
    'ST1007',
    'Vincent',
    'Lichakane',
    '2005-08-10',
    'brian.mokoena@ctucareer.edu',
    1
);


/*
Practical 2 – AFTER UPDATE Trigger
User Story

As a Registrar, I want the database to notify me whenever student information is updated 
so that changes can be monitored. */

CREATE TRIGGER trgStudentUpdated

ON Students

AFTER UPDATE

AS
BEGIN

    PRINT 'Student information has been updated.';

END;
GO

-- Test Update Trigger 
UPDATE Students

SET Email='aruna.l@ctucareer.edu'

WHERE StudentID=6;


/*
Practical 3 – INSTEAD OF DELETE Trigger
User Story

As a Principal, I want to prevent accidental deletion of student records 
so that important student information is never lost. */

CREATE TRIGGER trgPreventStudentDelete

ON Students

INSTEAD OF DELETE

AS
BEGIN

    PRINT 'Student records cannot be deleted.';

END;
GO


-- Test delete trigger
DELETE

FROM Students

WHERE StudentID=1;

/* The DELETE statement never happened because the Trigger replaced it.*/



/*
Practical 4 – Trigger with Business Rule
User Story

As a Database Administrator, I want inactive students to be prevented 
from enrolling in new courses so that only active students can register.*/


CREATE TRIGGER trgPreventInactiveEnrollment

ON Enrollments

INSTEAD OF INSERT

AS
BEGIN

    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        INNER JOIN Students s
            ON i.StudentID = s.StudentID
        WHERE s.IsActive = 0
    )
    BEGIN
        RAISERROR ('Inactive students cannot be enrolled.',16,1);
        RETURN;
    END

    INSERT INTO Enrollments
    (
        StudentID,
        CourseID,
        EnrollmentDate
    )
    SELECT
        StudentID,
        CourseID,
        EnrollmentDate
    FROM inserted;

END;
GO


-- Test for Innactive Student
INSERT INTO Enrollments
(
    StudentID,
    CourseID
)

VALUES
(
    4,
    1
);


-- Example: Test with an Active Student

INSERT INTO Enrollments
(
    StudentID,
    CourseID
)

VALUES
(
    6,
    2
);

-- verify the test:
SELECT *
FROM Enrollments
WHERE StudentID = 6;


/*Check runing Trigger */

SELECT
    name,
    parent_class_desc
FROM sys.triggers;


SELECT
    name,
    OBJECT_NAME(parent_id) AS TableName
FROM sys.triggers;