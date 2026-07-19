USE SchoolDB

GO

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
    'aruna.lusu@ctucareer.co.za',
    1
);

/* display students */

SELECT * FROM Students

/* Update 

The registrar informs us that Aruna changed his email address.

*/

UPDATE Students
SET Email='aruna.l@ctucareer.co.za'
WHERE StudentNumber='ST1006';

/* Display Aruna information */

SELECT *
FROM Students
WHERE StudentNumber='ST1006';


/* Never excute an Update class without a Where clause 

unless you intedn to update every record */

UPDATE Students
SET IsActive=0;

/* Undo it */
UPDATE Students
SET IsActive=1;


/* Example Aruna withdaws from college */
DELETE
FROM Students
WHERE StudentNumber='ST1006';

/* Delete class removes data permanently */