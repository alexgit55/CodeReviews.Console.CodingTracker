using Spectre.Console;
using System.Globalization;
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
    private static DateTime[] GetDateInputs()
    {
        var startDateInput = AnsiConsole.Ask<string>("Input Start Date with the format: dd-mm-yy hh:mm (24 hour clock). Or enter 0 to return to main menu.");

        if (startDateInput == "0") MainMenu();

        DateTime startDate;
        while (!DateTime.TryParseExact(startDateInput, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
        {
            startDateInput = AnsiConsole.Ask<string>("\n\nInvalid date. Format: dd-mm-yy hh:mm (24 hour clock). PLease try again\n\n");
        }

        var endDateInput = AnsiConsole.Ask<string>("Input End Date with the format: dd-mm-yy hh:mm (24 hour clock). Or enter 0 to return to main menu.");

        if (endDateInput == "0") MainMenu();

        DateTime endDate;
        while (!DateTime.TryParseExact(endDateInput, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
        {
            endDateInput = AnsiConsole.Ask<string>("\n\nInvalid date. Format: dd-mm-yy hh:mm (24 hour clock). PLease try again\n\n");
        }

        while (startDate > endDate)
        {
            endDateInput = AnsiConsole.Ask<string>("\n\nEnd date can't be before start date. Please try again\n\n");

            while (!DateTime.TryParseExact(endDateInput, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                endDateInput = AnsiConsole.Ask<string>("\n\nInvalid date. Format: dd-mm-yy hh:mm (24 hour clock). PLease try again\n\n");
            }
        }

        return [startDate, endDate];
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
        CodingSession record = new();

        var dateInputs = GetDateInputs();
        record.DateStart = dateInputs[0];
        record.DateEnd = dateInputs[1];

        var dataAccess = new DataAccess();
        dataAccess.InsertRecord(record);
    }
}   
