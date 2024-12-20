using Spectre.Console;
using static CodingTracker.alexgit55.Enums;

namespace CodingTracker.alexgit55;

internal class UserInterface
{
    internal static void MainMenu()
    {
        var isMenuRunning = true;
        var menuMessage = "Press any key to continue";

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

            AnsiConsole.MarkupLine($"\n{menuMessage}\n");
            Console.ReadKey();
            Console.Clear();
        }
    }
    private static DateTime[] GetDateInputs()
    {
        var startDateInput = AnsiConsole.Ask<string>("Input Start Date with the format: dd-mm-yy hh:mm (24 hour clock). Or enter 0 to return to main menu.");

        if (startDateInput == "0") MainMenu();

        var startDate = Validation.ValidateStartDate(startDateInput);

        var endDateInput = AnsiConsole.Ask<string>("Input End Date with the format: dd-mm-yy hh:mm (24 hour clock). Or enter 0 to return to main menu.");

        if (endDateInput == "0") MainMenu();

        var endDate = Validation.ValidateEndDate(startDate, endDateInput);

        return [startDate, endDate];
    }

    private static void DeleteSession()
    {
        var dataAccess = new DataAccess();
        var sessions = dataAccess.GetAllSessions();
        ViewSessions(sessions);

        var id = GetNumber("Enter the Id of the session you want to delete. Or enter 0 to return to main menu: ");

        if (id == 0) return;

        if (!Validation.ValidateSessionExists(sessions, id))
        {
            AnsiConsole.MarkupLine($"\nSession with id {id} not found.\n");
            return;
        }

        if (AnsiConsole.Prompt(new ConfirmationPrompt("Are you sure you want to delete this session?")))
        {
            dataAccess.DeleteSession(id);
            AnsiConsole.MarkupLine($"\nSession with id {id} has been deleted.\n");
        }
        else
            AnsiConsole.MarkupLine("\n[yellow]No session will be deleted.[/]\n");
    }

    private static void UpdateSession()
    {
        var dataAccess = new DataAccess();
        var sessions = dataAccess.GetAllSessions();
        var session = new CodingSession();
        ViewSessions(sessions);

        var id = GetNumber("Enter the Id of the session you want to update. Or enter 0 to return to main menu: ");

        if (id == 0) return;

        if (!Validation.ValidateSessionExists(sessions, id))
        {
            AnsiConsole.MarkupLine($"\nSession with id {id} not found.\n");
            return;
        }

        if (AnsiConsole.Prompt(new ConfirmationPrompt("Are you sure you want to update this session?")))
        {
            var dateInputs = GetDateInputs();
            session.DateStart = dateInputs[0];
            session.DateEnd = dateInputs[1];
            dataAccess.UpdateSession(session);
            AnsiConsole.MarkupLine($"\nSession with id {id} has been updated.\n");
        }
        else
            AnsiConsole.MarkupLine("\n[yellow]No session will be updated.[/]\n");
    }

    private static int GetNumber(string message)
    {
        var number = AnsiConsole.Prompt(
                new TextPrompt<int>(message)
                    .Validate(number =>
                    {
                        if (number < 0)
                            return ValidationResult.Error("[red]Please enter a non-negative number[/]");
                        return ValidationResult.Success();
                    })
        );
        return number;
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
