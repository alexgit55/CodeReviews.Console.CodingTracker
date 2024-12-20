// Purpose: This file contains the SeedData class which is used to seed the database with records for testing purposes.

namespace CodingTracker.alexgit55;

internal static class SeedData
{
    internal static void SeedRecords(int count)
    {
        Random random = new();
        DateTime currentDate = DateTime.Now.Date; // Start with today's date

        List<CodingSession> sessions = new List<CodingSession>();

        for (int i = 1; i <= count; i++)
        {
            DateTime startDate = currentDate.AddHours(random.Next(13));
            DateTime endDate = startDate.AddHours(random.Next(13));

            sessions.Add(new CodingSession
            {
                Id = i,
                DateStart = startDate,
                DateEnd = endDate,
            });

            // Increment the date for the next record
            currentDate = currentDate.AddDays(1);
        }

        var dataAccess = new DataAccess();
        dataAccess.BulkInsertSessions(sessions);
    }
}
