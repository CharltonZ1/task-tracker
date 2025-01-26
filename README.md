# Task Tracker CLI

A simple command-line interface (CLI) application built with .NET 9 to manage your tasks.  This tool allows you to add, update, delete, and track the progress of your tasks, storing them persistently in a JSON file. 

[Task Tracker Project](https://roadmap.sh/projects/task-tracker) created as part of the roadmap.sh Backend Developer path.

## Features

* **Add tasks:** Add new tasks with descriptions.
* **Update tasks:** Modify the description of existing tasks.
* **Delete tasks:** Remove tasks that are no longer needed.
* **Mark tasks as in-progress or done:** Track the status of your tasks.
* **List tasks:** View all tasks or filter by status (todo, in-progress, done).


## Building and Running

**Prerequisites:**

* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download) installed on your system.


**Build:**

1. Clone this repository: `git clone https://github.com/CharltonZ1/task-tracker.git`
2. Navigate to the project directory: `cd task-tracker/TaskTracker`
3. Build the application: `dotnet build`


**Run:**

1. Navigate to the output directory (usually `bin/Debug/net9.0`):  `cd bin/Debug/net9.0`
2. Open the output directory in your terminal or command prompt.
3. Run the application with a command: `./TaskTracker.exe [command] [options]`  (See Example PowerShell Usage below)

**Usage:**


```
.\TaskTracker.exe [command] [options]


Commands:

  add [description]                 Adds a new task with the given description.
  update [id] [description]        Updates the description of the task with the given ID.
  delete [id]                      Deletes the task with the given ID.
  mark-in-progress [id]           Marks the task with the given ID as in-progress.
  mark-done [id]                   Marks the task with the given ID as done.
  list [status]                    Lists all tasks. Optionally filter by status (todo, in-progress, or done).


Examples:


  Add a task:
    TaskTracker.exe add "Buy groceries"


  Update a task:
    TaskTracker.exe update 1 "Buy milk and bread"


  Delete a task:
    TaskTracker.exe delete 1


  Mark a task as in-progress:
   TaskTracker.exe mark-in-progress 1

  Mark a task as done:
    TaskTracker.exe mark-done 1


  List all tasks:
    TaskTracker.exe list


  List all tasks with status 'done':
     TaskTracker.exe list done

```

**Data Storage:**

Tasks are stored in a `data.json` file in the same directory as the executable.  This file will be created automatically if it does not exist.  Do not manually modify this file while the application is running as this might lead to data corruption.   



**License:**
MIT License



