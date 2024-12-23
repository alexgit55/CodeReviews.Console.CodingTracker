// This class is used to hold the data returned from the database for the session statistics.

namespace CodingTracker.alexgit55
{
    internal class SessionStats
    {
        internal int Id { get; set; }
        internal string TimePeriod { get; set; }
        internal double TotalTime { get; set; }
        internal double AverageSession { get; set; }
        internal double LongestSession { get; set; }
        internal double ShortestSession { get; set; }
        internal int TotalSessions { get; set; }
    }
}
