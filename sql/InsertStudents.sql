USE SchoolDB;
GO

INSERT INTO Students
(StudentNumber, FirstName, LastName, DateOfBirth, Email, IsActive)
VALUES
('ST1001', 'Amina', 'Selemani', '2004-05-14', 'amina.s@ctucareer.edu', 1),

('ST1002', 'Rosemary', 'WT', '2003-11-08', 'rose.wt@ctucareer.edu', 1),

('ST1003', 'David', 'Brown', '2005-01-25', NULL, 1),

('ST1004', 'Mandisa', 'Williams', '2002-09-19', 'mandisa.williams@ctucareer.edu', 0),

('ST1005', 'James', 'Wilson', '2004-07-30', NULL, 1);
GO