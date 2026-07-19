using Microsoft.Data.SqlClient;
using System.Data;

const string connectionString =
    "Server=.;" +
    "Database=SchoolDB;" +
    "Integrated Security=True;" +
    "Encrypt=True;" +
    "TrustServerCertificate=True;";

bool continueRunning = true;

while (continueRunning)
{
    Console.Clear();

    Console.WriteLine("======================================");
    Console.WriteLine("     SCHOOLDB MANAGEMENT SYSTEM");
    Console.WriteLine("======================================");
    Console.WriteLine("1. View all students");
    Console.WriteLine("2. Search for a student");
    Console.WriteLine("3. Add a new student");
    Console.WriteLine("4. Update a student");
    Console.WriteLine("5. Delete a student");
    Console.WriteLine("6. View all courses");
    Console.WriteLine("7. Enrol a student in a course");
    Console.WriteLine("8. View student enrolments");
    Console.WriteLine("0. Exit");
    Console.WriteLine("======================================");
    Console.Write("Select an option: ");

    string? option = Console.ReadLine();

    Console.Clear();

    switch (option)
    {
        case "1":
            ViewAllStudents();
            break;

        case "2":
            SearchStudent();
            break;

        case "3":
            AddStudent();
            break;

        case "4":
            UpdateStudent();
            break;

        case "5":
            DeleteStudent();
            break;

        case "6":
            ViewAllCourses();
            break;

        case "7":
            EnrolStudent();
            break;

        case "8":
            ViewStudentEnrolments();
            break;

        case "0":
            continueRunning = false;
            Console.WriteLine("Application closed.");
            break;

        default:
            Console.WriteLine("Invalid option.");
            Pause();
            break;
    }
}

// =====================================================
// READ: Display all students
// =====================================================

void ViewAllStudents()
{
    const string sql = """
        SELECT
            StudentID,
            StudentNumber,
            FirstName,
            LastName,
            DateOfBirth,
            Email,
            IsActive
        FROM Students
        ORDER BY FirstName, LastName;
        """;

    try
    {
        using SqlConnection connection = new(connectionString);
        using SqlCommand command = new(sql, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("STUDENT LIST");
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(
            $"{"ID",-5}" +
            $"{"Student No.",-15}" +
            $"{"Full Name",-25}" +
            $"{"Date of Birth",-17}" +
            $"{"Email",-28}" +
            $"{"Status",-10}"
        );

        Console.WriteLine(new string('-', 100));

        bool recordsFound = false;

        while (reader.Read())
        {
            recordsFound = true;

            int studentId = reader.GetInt32(
                reader.GetOrdinal("StudentID"));

            string studentNumber = reader.GetString(
                reader.GetOrdinal("StudentNumber"));

            string firstName = reader.GetString(
                reader.GetOrdinal("FirstName"));

            string lastName = reader.GetString(
                reader.GetOrdinal("LastName"));

            string dateOfBirth = reader.IsDBNull(
                reader.GetOrdinal("DateOfBirth"))
                ? "Not supplied"
                : reader.GetDateTime(
                    reader.GetOrdinal("DateOfBirth"))
                    .ToString("dd/MM/yyyy");

            string email = reader.IsDBNull(
                reader.GetOrdinal("Email"))
                ? "No email"
                : reader.GetString(
                    reader.GetOrdinal("Email"));

            bool isActive = reader.GetBoolean(
                reader.GetOrdinal("IsActive"));

            string status = isActive
                ? "Active"
                : "Inactive";

            Console.WriteLine(
                $"{studentId,-5}" +
                $"{studentNumber,-15}" +
                $"{firstName + " " + lastName,-25}" +
                $"{dateOfBirth,-17}" +
                $"{email,-28}" +
                $"{status,-10}"
            );
        }

        if (!recordsFound)
        {
            Console.WriteLine("No students were found.");
        }
    }
    catch (SqlException ex)
    {
        DisplayDatabaseError(ex);
    }

    Pause();
}

// =====================================================
// SEARCH: Execute stored procedure
// =====================================================

void SearchStudent()
{
    Console.Write("Enter the student number: ");
    string? studentNumber = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(studentNumber))
    {
        Console.WriteLine("Student number is required.");
        Pause();
        return;
    }

    try
    {
        using SqlConnection connection = new(connectionString);

        using SqlCommand command =
            new("dbo.GetStudentByNumber", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@StudentNumber",
            SqlDbType.VarChar,
            20
        ).Value = studentNumber.Trim();

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            string email = reader["Email"] == DBNull.Value
                ? "No email"
                : reader["Email"].ToString()!;

            string dateOfBirth =
                reader["DateOfBirth"] == DBNull.Value
                    ? "Not supplied"
                    : Convert.ToDateTime(
                        reader["DateOfBirth"])
                        .ToString("dd/MM/yyyy");

            bool isActive =
                Convert.ToBoolean(reader["IsActive"]);

            Console.WriteLine();
            Console.WriteLine("STUDENT FOUND");
            Console.WriteLine(new string('-', 42));

            Console.WriteLine(
                $"Student ID     : {reader["StudentID"]}");

            Console.WriteLine(
                $"Student Number : {reader["StudentNumber"]}");

            Console.WriteLine(
                $"Full Name      : {reader["FirstName"]} " +
                $"{reader["LastName"]}");

            Console.WriteLine(
                $"Date of Birth  : {dateOfBirth}");

            Console.WriteLine(
                $"Email          : {email}");

            Console.WriteLine(
                $"Status         : " +
                $"{(isActive ? "Active" : "Inactive")}");
        }
        else
        {
            Console.WriteLine(
                $"No student was found with number " +
                $"'{studentNumber}'.");
        }
    }
    catch (SqlException ex)
    {
        DisplayDatabaseError(ex);
    }

    Pause();
}

// =====================================================
// CREATE: Add a new student
// =====================================================

void AddStudent()
{
    Console.WriteLine("ADD A NEW STUDENT");
    Console.WriteLine(new string('-', 40));

    Console.Write("Student number: ");
    string? studentNumber = Console.ReadLine();

    Console.Write("First name: ");
    string? firstName = Console.ReadLine();

    Console.Write("Last name: ");
    string? lastName = Console.ReadLine();

    Console.Write("Date of birth (yyyy-MM-dd): ");
    string? dateInput = Console.ReadLine();

    Console.Write("Email address: ");
    string? email = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(studentNumber) ||
        string.IsNullOrWhiteSpace(firstName) ||
        string.IsNullOrWhiteSpace(lastName))
    {
        Console.WriteLine(
            "Student number, first name and last name are required.");

        Pause();
        return;
    }

    if (!DateTime.TryParse(dateInput, out DateTime dateOfBirth))
    {
        Console.WriteLine(
            "The date of birth is not valid.");

        Pause();
        return;
    }

    const string sql = """
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
            @StudentNumber,
            @FirstName,
            @LastName,
            @DateOfBirth,
            @Email,
            @IsActive
        );
        """;

    try
    {
        using SqlConnection connection = new(connectionString);
        using SqlCommand command = new(sql, connection);

        command.Parameters.Add(
            "@StudentNumber",
            SqlDbType.VarChar,
            20
        ).Value = studentNumber.Trim();

        command.Parameters.Add(
            "@FirstName",
            SqlDbType.VarChar,
            50
        ).Value = firstName.Trim();

        command.Parameters.Add(
            "@LastName",
            SqlDbType.VarChar,
            50
        ).Value = lastName.Trim();

        command.Parameters.Add(
            "@DateOfBirth",
            SqlDbType.Date
        ).Value = dateOfBirth.Date;

        command.Parameters.Add(
            "@Email",
            SqlDbType.VarChar,
            100
        ).Value = string.IsNullOrWhiteSpace(email)
            ? DBNull.Value
            : email.Trim();

        command.Parameters.Add(
            "@IsActive",
            SqlDbType.Bit
        ).Value = true;

        connection.Open();

        int rowsAffected = command.ExecuteNonQuery();

        Console.WriteLine();
        Console.WriteLine(
            $"{rowsAffected} student record added successfully.");
    }
    catch (SqlException ex) when (
        ex.Number == 2601 || ex.Number == 2627)
    {
        Console.WriteLine(
            "The student number already exists.");
    }
    catch (SqlException ex)
    {
        DisplayDatabaseError(ex);
    }

    Pause();
}

// =====================================================
// UPDATE: Update student information
// =====================================================

void UpdateStudent()
{
    Console.WriteLine("UPDATE STUDENT");
    Console.WriteLine(new string('-', 40));

    Console.Write("Enter the student number: ");
    string? studentNumber = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(studentNumber))
    {
        Console.WriteLine("Student number is required.");
        Pause();
        return;
    }

    Console.Write("Enter the new email address: ");
    string? newEmail = Console.ReadLine();

    Console.Write("Set student as active? (Y/N): ");
    string? activeInput = Console.ReadLine();

    bool isActive =
        string.Equals(
            activeInput,
            "Y",
            StringComparison.OrdinalIgnoreCase);

    const string sql = """
        UPDATE Students
        SET
            Email = @Email,
            IsActive = @IsActive
        WHERE StudentNumber = @StudentNumber;
        """;

    try
    {
        using SqlConnection connection = new(connectionString);
        using SqlCommand command = new(sql, connection);

        command.Parameters.Add(
            "@Email",
            SqlDbType.VarChar,
            100
        ).Value = string.IsNullOrWhiteSpace(newEmail)
            ? DBNull.Value
            : newEmail.Trim();

        command.Parameters.Add(
            "@IsActive",
            SqlDbType.Bit
        ).Value = isActive;

        command.Parameters.Add(
            "@StudentNumber",
            SqlDbType.VarChar,
            20
        ).Value = studentNumber.Trim();

        connection.Open();

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine(
                "Student information updated successfully.");
        }
        else
        {
            Console.WriteLine(
                "No matching student was found.");
        }
    }
    catch (SqlException ex)
    {
        DisplayDatabaseError(ex);
    }

    Pause();
}

// =====================================================
// DELETE: Trigger will prevent this operation
// =====================================================

void DeleteStudent()
{
    Console.WriteLine("DELETE STUDENT");
    Console.WriteLine(new string('-', 40));

    Console.Write("Enter the student number: ");
    string? studentNumber = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(studentNumber))
    {
        Console.WriteLine("Student number is required.");
        Pause();
        return;
    }

    Console.Write(
        $"Are you sure you want to delete " +
        $"{studentNumber}? (Y/N): ");

    string? confirmation = Console.ReadLine();

    if (!string.Equals(
        confirmation,
        "Y",
        StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Deletion cancelled.");
        Pause();
        return;
    }

    const string sql = """
        DELETE FROM Students
        WHERE StudentNumber = @StudentNumber;
        """;

    try
    {
        using SqlConnection connection = new(connectionString);
        using SqlCommand command = new(sql, connection);

        command.Parameters.Add(
            "@StudentNumber",
            SqlDbType.VarChar,
            20
        ).Value = studentNumber.Trim();

        connection.Open();

        int rowsAffected = command.ExecuteNonQuery();

        /*
         The INSTEAD OF DELETE trigger prevents the deletion.
         SQL Server may still report the attempted operation,
         so verify whether the student still exists.
        */

        bool studentStillExists =
            StudentExists(studentNumber.Trim());

        if (studentStillExists)
        {
            Console.WriteLine(
                "The student was not deleted.");

            Console.WriteLine(
                "Business rule: student records cannot be deleted.");
        }
        else if (rowsAffected > 0)
        {
            Console.WriteLine(
                "Student deleted successfully.");
        }
        else
        {
            Console.WriteLine(
                "No matching student was found.");
        }
    }
    catch (SqlException ex)
    {
        DisplayDatabaseError(ex);
    }

    Pause();
}

// =====================================================
// RELATIONSHIP: Display courses
// =====================================================

void ViewAllCourses()
{
    const string sql = """
        SELECT
            CourseID,
            CourseCode,
            CourseName,
            Credits
        FROM Courses
        ORDER BY CourseName;
        """;

    try
    {
        using SqlConnection connection = new(connectionString);
        using SqlCommand command = new(sql, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("COURSE LIST");
        Console.WriteLine(new string('-', 70));

        Console.WriteLine(
            $"{"ID",-5}" +
            $"{"Code",-15}" +
            $"{"Course Name",-35}" +
            $"{"Credits",-10}"
        );

        Console.WriteLine(new string('-', 70));

        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader["CourseID"],-5}" +
                $"{reader["CourseCode"],-15}" +
                $"{reader["CourseName"],-35}" +
                $"{reader["Credits"],-10}"
            );
        }
    }
    catch (SqlException ex)
    {
        DisplayDatabaseError(ex);
    }

    Pause();
}

// =====================================================
// RELATIONSHIP: Enrol a student in a course
// =====================================================

void EnrolStudent()
{
    Console.WriteLine("ENROL STUDENT IN A COURSE");
    Console.WriteLine(new string('-', 40));

    Console.Write("Enter Student ID: ");
    string? studentInput = Console.ReadLine();

    Console.Write("Enter Course ID: ");
    string? courseInput = Console.ReadLine();

    if (!int.TryParse(studentInput, out int studentId) ||
        !int.TryParse(courseInput, out int courseId))
    {
        Console.WriteLine(
            "Student ID and Course ID must be valid numbers.");

        Pause();
        return;
    }

    const string sql = """
        INSERT INTO Enrollments
        (
            StudentID,
            CourseID,
            EnrollmentDate
        )
        VALUES
        (
            @StudentID,
            @CourseID,
            GETDATE()
        );
        """;

    try
    {
        using SqlConnection connection = new(connectionString);
        using SqlCommand command = new(sql, connection);

        command.Parameters.Add(
            "@StudentID",
            SqlDbType.Int
        ).Value = studentId;

        command.Parameters.Add(
            "@CourseID",
            SqlDbType.Int
        ).Value = courseId;

        connection.Open();

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine(
                "Student enrolled successfully.");
        }
    }
    catch (SqlException ex) when (ex.Number == 50000)
    {
        Console.WriteLine();
        Console.WriteLine("BUSINESS RULE VIOLATION");
        Console.WriteLine(ex.Message);
    }
    catch (SqlException ex) when (ex.Number == 547)
    {
        Console.WriteLine();
        Console.WriteLine(
            "The student or course does not exist.");

        Console.WriteLine(
            "The foreign key relationship rejected the enrolment.");
    }
    catch (SqlException ex)
    {
        DisplayDatabaseError(ex);
    }

    Pause();
}

// =====================================================
// RELATIONSHIP: Display student-course relationships
// =====================================================

void ViewStudentEnrolments()
{
    const string sql = """
        SELECT
            S.StudentNumber,
            S.FirstName,
            S.LastName,
            C.CourseCode,
            C.CourseName,
            E.EnrollmentDate
        FROM Students AS S
        INNER JOIN Enrollments AS E
            ON S.StudentID = E.StudentID
        INNER JOIN Courses AS C
            ON E.CourseID = C.CourseID
        ORDER BY
            S.StudentNumber,
            C.CourseName;
        """;

    try
    {
        using SqlConnection connection = new(connectionString);
        using SqlCommand command = new(sql, connection);

        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("STUDENT ENROLMENTS");
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(
            $"{"Student No.",-15}" +
            $"{"Student Name",-25}" +
            $"{"Course Code",-15}" +
            $"{"Course Name",-30}" +
            $"{"Date",-12}"
        );

        Console.WriteLine(new string('-', 100));

        while (reader.Read())
        {
            DateTime enrolmentDate =
                Convert.ToDateTime(reader["EnrollmentDate"]);

            Console.WriteLine(
                $"{reader["StudentNumber"],-15}" +
                $"{reader["FirstName"] + " " + reader["LastName"],-25}" +
                $"{reader["CourseCode"],-15}" +
                $"{reader["CourseName"],-30}" +
                $"{enrolmentDate:dd/MM/yyyy}"
            );
        }
    }
    catch (SqlException ex)
    {
        DisplayDatabaseError(ex);
    }

    Pause();
}

// =====================================================
// Helper: Verify whether a student exists
// =====================================================

bool StudentExists(string studentNumber)
{
    const string sql = """
        SELECT COUNT(*)
        FROM Students
        WHERE StudentNumber = @StudentNumber;
        """;

    using SqlConnection connection = new(connectionString);
    using SqlCommand command = new(sql, connection);

    command.Parameters.Add(
        "@StudentNumber",
        SqlDbType.VarChar,
        20
    ).Value = studentNumber;

    connection.Open();

    int count = Convert.ToInt32(
        command.ExecuteScalar());

    return count > 0;
}

// =====================================================
// Helper: Display database errors
// =====================================================

void DisplayDatabaseError(SqlException ex)
{
    Console.WriteLine();
    Console.WriteLine("A database error occurred.");
    Console.WriteLine($"Error number: {ex.Number}");
    Console.WriteLine($"Message: {ex.Message}");
}

// =====================================================
// Helper: Pause before returning to the menu
// =====================================================

void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
}