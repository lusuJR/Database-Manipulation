USE SchoolDB

GO


/* A Stored Procedure is a saved collection of SQL statements 
that performs a specific task. 
It can accept parameters, making it reusable and easier to maintain. */


-- Create a Stored Procedure
CREATE PROCEDURE GetAllStudents
AS
BEGIN

    SELECT *
    FROM Students;

END;
GO

-- Execute the Procedure
EXEC GetAllStudents;


/* User story : 
As a Registrar, I want to search for a student using their student number so that 
I can quickly retrieve the student's information 
without writing SQL queries every time. */

-- Then start codin:

CREATE PROCEDURE GetStudentByNumber

@StudentNumber VARCHAR(20)

AS
BEGIN

    SELECT *
    FROM Students
    WHERE StudentNumber = @StudentNumber;

END;
GO


-- Run Procedure
EXEC GetStudentByNumber
    @StudentNumber = 'ST1002';

--User story : Search using First Name.

CREATE PROCEDURE GetStudentByFirstName

@FirstName VARCHAR(50)

AS
BEGIN

SELECT *

FROM Students

WHERE FirstName=@FirstName;

END;
GO

--Run  Procedure 
EXEC GetStudentByFirstName

@FirstName='Amina';


/*
Views help us save queries, while Stored Procedures allow us to save complete SQL
operations that can accept input from users. 
Instead of rewriting SQL every day, professional database developers 
create reusable procedures that make applications faster, 
easier to maintain and more secure.

*/