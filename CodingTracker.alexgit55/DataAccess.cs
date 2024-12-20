using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CodingTracker.alexgit55;

internal class DataAccess
{
    IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("C:\\Scripts\\GitHub\\alexgit55\\CodeReviews.Console.CodingTracker\\CodingTracker.alexgit55\\appsettings.json")
            .Build();

    private string ConnectionString;
    public DataAccess()
    {
        ConnectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    }
    internal void CreateDatabase()
    //Initializes the database and creates the table if it doesn't exist
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS records (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                DateStart TEXT NOT NULL,
                DateEnd TEXT NOT NULL
            )";

            connection.Execute(createTableQuery);
        }
    }

    internal void InsertRecord(CodingSession record)
    //Inserts a coding session into the database
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            string insertRecordQuery = @"
            INSERT INTO records (DateStart, DateEnd)
            VALUES (@DateStart, @DateEnd)";
            connection.Execute(insertRecordQuery, new { record.DateStart, record.DateEnd });
        }
    }

    internal IEnumerable<CodingSession> GetAllRecords()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            string selectQuery = "SELECT * FROM records";

            var records = connection.Query<CodingSession>(selectQuery);

            foreach (var record in records)
            {
                record.Duration = record.DateEnd - record.DateStart;
            }

            return records;
        }
    }
}
