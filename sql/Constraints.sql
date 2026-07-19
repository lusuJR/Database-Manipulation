USE SchoolDB

GO

INSERT INTO Enrollments
(
StudentID,
CourseID
)

VALUES
(
99,
1
);


/* Display students : First, find an existing student: */

SELECT * FROM Students

/* Now insert a valid enrollment: */ 

INSERT INTO Enrollments
(
    StudentID,
    CourseID
)
VALUES
(
    6,
    4
);


/*
A Foreign Key ensures that every value in a child table must already exist in the related parent table. 
This maintains referential integrity and prevents invalid or orphaned records in the database.

*/