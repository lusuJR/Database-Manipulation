# SchoolDB Database Design

## Overview

The SchoolDB database is designed to manage student registrations, course information and student enrolments. The database follows relational database design principles and uses primary and foreign keys to maintain data integrity.

---

## Database Tables

### Students

| Column | Description |
|---------|-------------|
| StudentID | Primary Key |
| StudentNumber | Unique student number |
| FirstName | Student first name |
| LastName | Student last name |
| DateOfBirth | Student date of birth |
| Email | Student email |
| IsActive | Student status |

---

### Courses

| Column | Description |
|---------|-------------|
| CourseID | Primary Key |
| CourseCode | Course code |
| CourseName | Course name |
| Credits | Course credits |

---

### Enrollments

| Column | Description |
|---------|-------------|
| EnrollmentID | Primary Key |
| StudentID | Foreign Key |
| CourseID | Foreign Key |
| EnrollmentDate | Date of enrolment |

---

## Entity Relationship Diagram

<img width="767" height="234" alt="image" src="https://github.com/user-attachments/assets/f6a3f92e-5a03-46d5-92a1-8f9da10cd40a" />


---

## Relationships

- One Student can have many Enrollments.
- One Course can have many Enrollments.
- The Enrollments table resolves the many-to-many relationship between Students and Courses.

---

## Database Objects

### Views
- vwActiveStudents
- vwStudentCourses

### Stored Procedures
- GetAllStudents
- GetStudentByNumber
- GetStudentByFirstName

### User-Defined Functions
- fnTotalStudents
- fnFullName
- fnCalculateAge
- fnStudentsByCourse

### Triggers
- trgStudentInserted
- trgStudentUpdated
- trgPreventStudentDelete
- trgPreventInactiveEnrollment
