// Purpose: Contains the Enums used in the program.

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodingTracker.alexgit55;

internal class Enums
{
    internal enum  MainMenuChoices
    {
        [Description("Add Session")]
        AddSession,

        [Description("View Sessions")]
        ViewSessions,

        [Description("View Stats")]
        ViewStats,

        [Description("Start Coding Session")]
        StartSession,

        [Description("Delete Session")]
        DeleteSession,

        [Description("Update Session")]
        UpdateSession,

        Exit
    }

    internal enum  TimePeriod
    {
        [Description("All")]
        All,

        [Description("By Day")]
        ByDay,

        [Description("By Week")]
        ByWeek,

        [Description("By Month")]
        ByMonth,

        [Description("By Year")]
        ByYear,

        [Description("Return to Main Menu")]
        MainMenu
    }
}
