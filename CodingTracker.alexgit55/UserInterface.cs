using Spectre.Console;
using static CodingTracker.alexgit55.Enums;

namespace CodingTracker.alexgit55;

internal class UserInterface
{
    internal static void MainMenu()
    {
        var isMenuRunning = true;
        var menuMessage= "Press any key to continue";

        while (isMenuRunning)
        {
            var usersChoice = AnsiConsole.Prompt(
                   new SelectionPrompt<MainMenuChoices>()
                    .Title("What would you like to do?")
                    .AddChoices(
                       MainMenuChoices.AddRecord,
                       MainMenuChoices.ViewRecords,
                       MainMenuChoices.UpdateRecord,
                       MainMenuChoices.DeleteRecord,
                       MainMenuChoices.Exit)
                    );

            switch (usersChoice)
            {
                case MainMenuChoices.AddRecord:
                    AddRecord();
                    break;
                case MainMenuChoices.ViewRecords:
                    ViewRecords();
                    break;
                case MainMenuChoices.UpdateRecord:
                    UpdateRecord();
                    break;
                case MainMenuChoices.DeleteRecord:
                    DeleteRecord();
                    break;
                case MainMenuChoices.Exit:
                    isMenuRunning = false;
                    menuMessage = "Goodbye!";
                    break;
            }

            AnsiConsole.MarkupLine(menuMessage);
            Console.ReadKey();
        }
    }

    private static void DeleteRecord()
    {
        throw new NotImplementedException();
    }

    private static void UpdateRecord()
    {
        throw new NotImplementedException();
    }

    private static void ViewRecords()
    {
        throw new NotImplementedException();
    }

    private static void AddRecord()
    {
        throw new NotImplementedException();
    }
}   
