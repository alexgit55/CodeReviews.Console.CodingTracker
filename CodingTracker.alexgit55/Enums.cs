using System.ComponentModel.DataAnnotations;

namespace CodingTracker.alexgit55;

internal class Enums
{
    internal enum  MainMenuChoices
    {
        [Display(Name = "Add Session")]
        AddSession,

        [Display(Name = "View Sessions")]
        ViewSessions,

        [Display(Name = "Delete Session")]
        DeleteSession,

        [Display(Name = "Update Session")]
        UpdateSession,

        Exit
    }
}
