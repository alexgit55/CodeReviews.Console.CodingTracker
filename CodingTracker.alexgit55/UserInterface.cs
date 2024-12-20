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
            
            DisplayHeader("Welcome to the Coding Tracker!");


            var usersChoice = AnsiConsole.Prompt(
                   new SelectionPrompt<MainMenuChoices>()
                    .Title("What would you like to do?")
                    .AddChoices(
                       MainMenuChoices.AddSession,
                       MainMenuChoices.StartSession,
                       MainMenuChoices.ViewSessions,
                       MainMenuChoices.UpdateSession,
                       MainMenuChoices.DeleteSession,
                       MainMenuChoices.Exit)
                    );

            switch (usersChoice)
            {
                case MainMenuChoices.AddSession:
                    DisplayHeader("Add a Coding Session");
                    AddSession();
                    break;
                case MainMenuChoices.StartSession:
                    DisplayHeader("Start a Coding Session");
                    StartSession();
                    break;
                case MainMenuChoices.ViewSessions:
                    DisplayHeader("View Coding Sessions");
                    var dataAccess = new DataAccess();
                    var sessions = dataAccess.GetAllSessions();
                    ViewSessions(sessions);
                    break;
                case MainMenuChoices.UpdateSession:
                    DisplayHeader("Update a Coding Session");
                    UpdateSession();
                    break;
                case MainMenuChoices.DeleteSession:
                    DisplayHeader("Delete a Coding Session");
                    DeleteSession();
                    break;
                case MainMenuChoices.Exit:
                    isMenuRunning = false;
                    DisplayHeader("Thank you for using the Coding Tracker!");
                    menuMessage = "Goodbye!";
                    break;
            }

            AnsiConsole.MarkupLine($"\n{menuMessage}\n");
            Console.ReadKey();
            Console.Clear();
        }
    }
    
    private static void DisplayHeader(string message)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold green]------------------------------[/]");
        AnsiConsole.MarkupLine($"[bold green]{message}[/]");
        AnsiConsole.MarkupLine("[bold green]------------------------------[/]\n");
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

        var id = Validation.GetNumber("Enter the Id of the session you want to delete. Or enter 0 to return to main menu: ");

        if (id == 0) return;

        if (!Validation.ValidateSessionExists(sessions, id))
        {
            AnsiConsole.MarkupLine($"\n[red]Session with id {id} not found.[/]\n");
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

        var id = Validation.GetNumber("Enter the Id of the session you want to update. Or enter 0 to return to main menu: ");

        if (id == 0) return;

        if (!Validation.ValidateSessionExists(sessions, id))
        {
            AnsiConsole.MarkupLine($"\n[red]Session with id {id} not found.[/]\n");
            return;
        }

        if (AnsiConsole.Prompt(new ConfirmationPrompt("Are you sure you want to update this session?")))
        {
            var dateInputs = GetDateInputs();
            session.Id = id;
            session.DateStart = dateInputs[0];
            session.DateEnd = dateInputs[1];
            dataAccess.UpdateSession(session);
            AnsiConsole.MarkupLine($"\nSession with id {id} has been updated.\n");
        }
        else
            AnsiConsole.MarkupLine("\n[yellow]No session will be updated.[/]\n");
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
            table.AddRow(session.Id.ToString(), session.DateStart.ToString(), session.DateEnd.ToString(), $"{session.Duration.Hours} hours {session.Duration.Minutes} minutes");
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

    private static void StartSession()
    {
        AnsiConsole.MarkupLine("This will start a live coding session and will record the total time afterwards.\n");

        AnsiConsole.MarkupLine("Press any key to start the session.");
        Console.ReadKey();
        var continueSession = true;
        var startTime = DateTime.Now;

        while (continueSession)
        {
            DisplayHeader("Coding Session in Progress");
            continueSession = AnsiConsole.Prompt(new ConfirmationPrompt("Continue coding session?"));
        }

        var endTime = DateTime.Now;

        AnsiConsole.MarkupLine($"Session started at {startTime} and ended at {endTime}.");

        CodingSession session = new()
        {
            DateStart = startTime,
            DateEnd = endTime
        };

        var dataAccess = new DataAccess();
        dataAccess.InsertSession(session);


    }
}
