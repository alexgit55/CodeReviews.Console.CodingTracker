// Purpose: Contains the DataAccess class which is responsible for interacting with the database.

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
            CREATE TABLE IF NOT EXISTS sessions (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                DateStart TEXT NOT NULL,
                DateEnd TEXT NOT NULL,
                Duration TEXT NOT NULL
            )";

            connection.Execute(createTableQuery);
        }
    }

    internal void InsertSession(CodingSession session)
    //Inserts a coding session into the database
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            string insertRecordQuery = @"
            INSERT INTO sessions (DateStart, DateEnd, Duration)
            VALUES (@DateStart, @DateEnd, @Duration)";
            connection.Execute(insertRecordQuery, new { session.DateStart, session.DateEnd, session.Duration });
        }
    }

    internal IEnumerable<CodingSession> GetAllSessions()
    //Returns all coding sessions from the database
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            string selectQuery = "SELECT * FROM sessions";

            var sessions = connection.Query<CodingSession>(selectQuery);

            /*foreach (var session in sessions)
            {
                session.Duration = session.DateEnd - session.DateStart;
            }*/

            return sessions;
        }
    }

    internal void BulkInsertSessions(List<CodingSession> sessions)
    // Inserts multiple sessions into the database
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            // Prepare the query with placeholders for multiple sessions
            string insertQuery = @"
            INSERT INTO sessions (DateStart, DateEnd, Duration)
            VALUES (@DateStart, @DateEnd, @Duration)";

            // Execute the query for each session in the collection
            connection.Execute(insertQuery, sessions.Select(session => new
            {
                session.DateStart,
                session.DateEnd,
                session.Duration
            }));
        }
    }

    internal void UpdateSession(CodingSession session)
    // Updates a coding session in the database
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            string updateQuery = @"
            UPDATE sessions
            SET DateStart = @DateStart, DateEnd = @DateEnd, Duration = @Duration
            WHERE Id = @Id";

            connection.Execute(updateQuery, new { session.DateStart, session.DateEnd, session.Duration, session.Id });
        }
    }

    internal void DeleteSession(int id)
    // Deletes a coding session from the database
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            string deleteQuery = @"
            DELETE FROM sessions
            WHERE Id = @Id";
            connection.Execute(deleteQuery, new { Id = id });
        }

    }
}
