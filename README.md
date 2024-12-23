# Coding Tracker App
My 4th project for the C# Academy and second using an SQLite database

Advances over the Habit Tracker app is that this one is focused on Object Oriented Programming and Separation of Concerns

Console based CRUD application to track coding hours. Developed using C# and SQLite.

## Given Requirements:

- This application has the same requirements as the previous project, except that now you'll be logging your daily coding time.

- We should use Spectre.Console to print tables and collect inputs.

- You're required to have separate classes in different files. ie. UserInput.cs, Validation.cs, CodingController.cs).

- You should tell the user the specific format you want the date and time to be logged and not allow any other format.

- You'll need to create a configuration file that will contain your database path and connection strings.

- You'll need to create a CodingSession class or record in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration.

- The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate CalculateDuration method.

- When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.

- For database interactions we need to use Dapper.

## Challenges

- Add the possibility of tracking the coding time via a stopwatch so the user can track the session as it happens. (Completed)

- Let the users filter their coding records per period (weeks, days, years) and/or order ascending or descending. (TBD)

- Create reports where the users can see their total and average coding session per period. (Completed)

- Create the ability to set coding goals and show how far the users are from reaching their goal, along with how many hours a day they would have to code to reach their goal. You can do it via SQL queries or with C#. (TBD)

## Features

- SQLite Database connection using Dapper
- This program uses an SQLite database to store and access information
- If no database/table exits when starting the program, it will create one automatically.

## Console Based UI

- This program features a text based menu and navigation system to access its function
- It utilizes the Spectre.Console library to generate the main menu and display text and sessions in the program
  ![Screenshot of the main menu of the application.](https://rvnprojectstorage.blob.core.windows.net/images/Console.CodingTracker/MainMenu.png)

## CRUD DB Functions

- This program offers CRUD operations (Create, Read, Update, Delete) for any session entered
- The date and coding times need to be in dd-mm-yy hh:mm (24 hour clock) format
- The end date must be a later time than the start date
- Here are screenshots from some of the various operations
  |View Sessions|Add Session|
  |:-:|:-:|
  |![Add a coding session](https://rvnprojectstorage.blob.core.windows.net/images/Console.CodingTracker/ViewSessions.png)|![View coding sessions](https://rvnprojectstorage.blob.core.windows.net/images/Console.CodingTracker/AddSession.png)|
  |Update Session|Remove Session|
  |![Update a coding session](https://rvnprojectstorage.blob.core.windows.net/images/Console.CodingTracker/UpdateSession.png)|![Remove a coding session](https://rvnprojectstorage.blob.core.windows.net/images/Console.CodingTracker/DeleteSession.png)|

## Live Coding Session

- This program features the ability to start a stopwatch for a live coding session
![Start a new coding session](https://rvnprojectstorage.blob.core.windows.net/images/Console.CodingTracker/StartSession.png)

## Session Statistics

- The program offers the ability the view stats about coding sessions over a time period (day, week, month, year or all)
- The stats are the total sessions, the average duration, the shortest duration, the longest duration and the total duration across the time period
 |Filter Menu|Example Filter View|
 |:-:|:-:|
 |![Filter Menu](https://rvnprojectstorage.blob.core.windows.net/images/Console.CodingTracker/SessionStatsFilter.png)|![Example Filter by Month](https://rvnprojectstorage.blob.core.windows.net/images/Console.CodingTracker/SessionStatsByMonth.png)|

  
