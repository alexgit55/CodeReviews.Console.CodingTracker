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
                       MainMenuChoices.AddSession,
                       MainMenuChoices.ViewSessions,
                       MainMenuChoices.UpdateSession,
                       MainMenuChoices.DeleteSession,
                       MainMenuChoices.Exit)
                    );

            switch (usersChoice)
            {
                case MainMenuChoices.AddSession:
                    AddSession();
                    break;
                case MainMenuChoices.ViewSessions:
                    var dataAccess = new DataAccess();
                    var sessions = dataAccess.GetAllSessions();
                    ViewSessions(sessions);
                    break;
                case MainMenuChoices.UpdateSession:
                    UpdateSession();
                    break;
                case MainMenuChoices.DeleteSession:
                    DeleteSession();
                    break;
                case MainMenuChoices.Exit:
                    isMenuRunning = false;
                    menuMessage = "Goodbye!";
                    break;
            }

            AnsiConsole.MarkupLine(menuMessage);
            Console.ReadKey();
            Console.Clear();
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

    private static void DeleteSession()
    {
        throw new NotImplementedException();
    }

    private static void UpdateSession()
    {
        throw new NotImplementedException();
    }

    private static void ViewSessions(IEnumerable<CodingSession> sessions)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Start Date");
        table.AddColumn("End Date");
        table.AddColumn("Duration");

        foreach (var session in sessions)
        {
            table.AddRow(session.Id.ToString(), session.DateStart.ToString(), session.DateEnd.ToString(), $"{session.Duration.TotalHours} hours {session.Duration.TotalMinutes % 60} minutes");
        }

        AnsiConsole.Write(table);
    }

    private static void AddSession()
    {
        CodingSession session = new();

        var dateInputs = GetDateInputs();
        session.DateStart = dateInputs[0];
        session.DateEnd = dateInputs[1];

        var dataAccess = new DataAccess();
        dataAccess.InsertSession(session);
    }
}   
