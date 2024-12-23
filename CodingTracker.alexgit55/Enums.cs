// Purpose: Contains the Enums used in the program.

namespace CodingTracker.alexgit55;

internal class Enums
{
    internal enum MainMenuChoices
    {
        AddSession,
        ViewSessions,
        ViewStats,
        StartSession,
        DeleteSession,
        UpdateSession,
        Exit
    }

    internal enum TimePeriod
    {
        All,
        ByDay,
        ByWeek,
        ByMonth,
        ByYear,
        MainMenu
    }
}
