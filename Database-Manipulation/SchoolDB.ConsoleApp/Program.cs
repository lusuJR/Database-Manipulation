using Microsoft.Data.SqlClient;

string connectionString =
    "Server=.;" +
    "Database=SchoolDB;" +
    "Integrated Security=True;" +
    "Encrypt=True;" +
    "TrustServerCertificate=True;";

try
{
    using SqlConnection connection = new(connectionString);

    Console.WriteLine("Connecting to SchoolDB...");
    connection.Open();
    Console.WriteLine("Connected successfully!\n");

    string sql = """
        SELECT
            StudentID,
            StudentNumber,
            FirstName,
            LastName,
            Email,
            IsActive
        FROM Students
        ORDER BY FirstName;
        """;

    using SqlCommand command = new(sql, connection);
    using SqlDataReader reader = command.ExecuteReader();

    Console.WriteLine("STUDENT LIST");
    Console.WriteLine(new string('-', 90));
    Console.WriteLine(
        $"{"ID",-5} {"Student No.",-15} {"Full Name",-25} {"Email",-30} {"Status",-10}"
    );
    Console.WriteLine(new string('-', 90));

    while (reader.Read())
    {
        int studentId = reader.GetInt32(0);
        string studentNumber = reader.GetString(1);
        string firstName = reader.GetString(2);
        string lastName = reader.GetString(3);

        string email = reader.IsDBNull(4)
            ? "No email"
            : reader.GetString(4);

        bool isActive = reader.GetBoolean(5);
        string status = isActive ? "Active" : "Inactive";

        Console.WriteLine(
            $"{studentId,-5} " +
            $"{studentNumber,-15} " +
            $"{firstName + " " + lastName,-25} " +
            $"{email,-30} " +
            $"{status,-10}"
        );
    }
}
catch (SqlException ex)
{
    Console.WriteLine("A database error occurred.");
    Console.WriteLine(ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine("An unexpected error occurred.");
    Console.WriteLine(ex.Message);
}

Console.WriteLine("\nPress any key to close...");
Console.ReadKey();