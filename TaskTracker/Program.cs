using TaskTracker.Classes;


//var temp1 = new ToDoTask("Task 1");
//var temp2 = new ToDoTask("Task 2");
//var temp3 = new ToDoTask("Task 3");


//tasks.Add(temp1);
//tasks.Add(temp2);
//tasks.Add(temp3);

//ToDoTask.WriteAll(tasks);

//var readTasks = ToDoTask.ReadAll();

//foreach (var task in readTasks)
//{
//    Console.WriteLine(task.Description);
//}

//// Prevent console from closing immediately
//Console.ReadLine();

// Commands for CLI

// add
// update
// delete
// mark-in-progress
// mark-done
// list by status

// first read our list of tasks to get the current state
// then we can add, update, delete, mark-in-progress, mark-done, list by status

var tasks = ToDoTask.ReadAll();

if (args.Length == 0)
{
    Console.WriteLine("Please provide a command");
    return;
}

var command = args[0];
var argCount = args.Length - 1;

Console.WriteLine($"Command: {command}");
Console.WriteLine($"Arguments: {argCount}");
Console.WriteLine($"Args: {string.Join(", ", args)}");

switch (command)
{
    case "add":
        if (argCount < 1 || argCount > 1)
        {
            Console.WriteLine(argCount < 2 ? "Please provide a description" : "Too many arguments");
            return;
        }
        var newTask = new ToDoTask(args[1]);
        tasks.Add(newTask);
        ToDoTask.WriteAll(tasks);
        Console.WriteLine($"Task successfully added (ID: {newTask.Id})");
        break;
    case "update":
        if (argCount < 2 || argCount > 2)
        {
            Console.WriteLine(argCount < 2 ? "Please provide an ID and a description" : "Too many arguments");
            return;
        }
        var updateId = int.Parse(args[1]);
        var updateTask = tasks.Find(t => t.Id == updateId);
        if (updateTask == null)
        {
            Console.WriteLine("Task not found");
            return;
        }
        updateTask.Description = args[2];
        ToDoTask.WriteAll(tasks);
        Console.WriteLine($"Task successfully updated (ID: {updateTask.Id})");
        break;
    case "delete":
        if (argCount < 1 || argCount > 1)
        {
            Console.WriteLine(argCount < 2 ? "Please provide an ID" : "Too many arguments");
            return;
        }
        var deleteId = int.Parse(args[1]);
        var deleteTask = tasks.Find(t => t.Id == deleteId);
        if (deleteTask == null)
        {
            Console.WriteLine("Task not found");
            return;
        }
        tasks.Remove(deleteTask);
        ToDoTask.WriteAll(tasks);
        Console.WriteLine($"Task successfully deleted (ID: {deleteTask.Id})");
        break;
    case "mark-in-progress":
        if (argCount < 1 || argCount > 1)
        {
            Console.WriteLine(argCount < 2 ? "Please provide an ID" : "Too many arguments");
            return;
        }
        var markInProgressId = int.Parse(args[1]);
        var markInProgressTask = tasks.Find(t => t.Id == markInProgressId);
        if (markInProgressTask == null)
        {
            Console.WriteLine("Task not found");
            return;
        }
        markInProgressTask.Status = StatusEnum.InProgress;
        ToDoTask.WriteAll(tasks);
        Console.WriteLine($"Task successfully marked in progress (ID: {markInProgressTask.Id})");
        break;

    case "mark-done":
        if (argCount < 1 || argCount > 1)
        {
            Console.WriteLine(argCount < 2 ? "Please provide an ID" : "Too many arguments");
            return;
        }
        var markDoneId = int.Parse(args[1]);
        var markDoneTask = tasks.Find(t => t.Id == markDoneId);
        if (markDoneTask == null)
        {
            Console.WriteLine("Task not found");
            return;
        }
        markDoneTask.Status = StatusEnum.Done;
        ToDoTask.WriteAll(tasks);
        Console.WriteLine($"Task successfully marked done (ID: {markDoneTask.Id})");
        break;

    case "list":
        if (argCount > 1)
        {
            Console.WriteLine("Too many arguments");
            return;
        } else if (argCount < 1)
        {
            // List all tasks
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found");
                return;
            }
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
            return;
        }
        var listStatus = args[1];
        // strip - from status
        if (listStatus.Contains('-'))
        {
            listStatus = listStatus.Replace("-", "");
        }
        var listTasks = tasks.FindAll(t => t.Status.ToString().ToLower() == listStatus.ToLower());
        if (listTasks.Count == 0)
        {
            Console.WriteLine("No tasks found");
            return;
        }
        foreach (var task in listTasks)
        {
            Console.WriteLine(task);
        }
        break;

    default:
        Console.WriteLine("Unknown command");
        break;
}

