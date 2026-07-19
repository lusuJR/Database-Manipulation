using Microsoft.Data.SqlClient;

string connectionString =
    "Server=.;" +
    "Database=SchoolDB;" +
    "Trusted_Connection=True;" +
    "TrustServerCertificate=True;";

try
{
    Console.WriteLine("Connecting to SchoolDB...");

    using SqlConnection connection = new(connectionString);

    connection.Open();

    Console.WriteLine("Connected successfully!");
}
catch (Exception ex)
{
    Console.WriteLine("Connection failed.");
    Console.WriteLine(ex.Message);
}