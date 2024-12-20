// Purpose: Contains the CodingSession class which is used to store information about a coding session.

namespace CodingTracker.alexgit55
{
    internal class CodingSession
    {
        internal int Id { get; set; }
        internal DateTime DateStart { get; set; }
        internal DateTime DateEnd { get; set; }
        internal string Duration { get; set; }

        internal void CalculateDuration()
        {
            TimeSpan duration = DateEnd - DateStart;
            Duration = duration.ToString();
        }
    }
}
