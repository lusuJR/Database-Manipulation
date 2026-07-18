CREATE TABLE Enrollments
(
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL,
    CourseID INT NOT NULL,
    EnrollmentDate DATE DEFAULT GETDATE(),

    FOREIGN KEY(StudentID)
        REFERENCES Students(StudentID),

    FOREIGN KEY(CourseID)
        REFERENCES Courses(CourseID)
);