using TaskTracker.Classes;

if (args.Length == 0)
{
    Console.WriteLine("Please provide a command");
    return;
}

var command = args[0];
var argCount = args.Length - 1;

handleCommandValidation(command, argCount, args);

switch (command.ToLower())
{
    case "add":
        ToDoTask.AddTask(args[1]);
        break;
    case "update":
        if (isNotNumber("update", args[1])) return;
        ToDoTask.UpdateTask(int.Parse(args[1]), args[2]);
        break;
    case "delete":
        if (isNotNumber("delete", args[1])) return;
        ToDoTask.DeleteTask(int.Parse(args[1]));
        break;
    case "mark-in-progress":
        if (isNotNumber("mark-in-progress", args[1])) return;
        ToDoTask.MarkInProgress(int.Parse(args[1]));
        break;
    case "mark-done":
        if (isNotNumber("mark-done", args[1])) return;
        ToDoTask.MarkDone(int.Parse(args[1]));
        break;
    case "list":
        if (argCount == 0)
        {
            ToDoTask.ListTasks();
        }
        else
        {
            // first strip dash from status (in-progress -> InProgress)
            var cleanedStatus = args[1].Replace("-", "");

            // try to parse status
            if (!Enum.TryParse<StatusEnum>(cleanedStatus, true, out var status))
            {
                Console.WriteLine("Invalid status");
                return;
            }

            ToDoTask.ListTasks(status);
        }
        break;
    default:
        Console.WriteLine("Unknown command");
        break;
}
static void handleCommandValidation(string command, int argCount, string[] parameters)
{
    switch (command)
    {
        case "add":
            if (argCount < 1 || argCount > 1)
            {
                var msg = argCount < 2 ? "Please provide a description" : "Too many arguments";
                Console.WriteLine("add: {0}", msg);
                return;
            }

            // check if description is empty
            if (string.IsNullOrWhiteSpace(parameters[1]))
            {
                Console.WriteLine("add: Description cannot be empty");
                return;
            }
            break;
        case "update":
            if (argCount < 2 || argCount > 2)
            {
                var msg = argCount < 2 ? "Please provide an ID and a description" : "Too many arguments";
                Console.WriteLine("update: {0}", msg);
                return;
            }
            // check if description is empty
            if (string.IsNullOrWhiteSpace(parameters[2]))
            {
                Console.WriteLine("update: Description cannot be empty");
                return;
            }
            break;
        case "delete":
            if (argCount < 1 || argCount > 1)
            {
                var msg = argCount < 2 ? "Please provide an ID" : "Too many arguments";
                Console.WriteLine("delete: {0}", msg);
                return;
            }
            break;
        case "mark-in-progress":
            if (argCount < 1 || argCount > 1)
            {
                var msg = argCount < 2 ? "Please provide an ID" : "Too many arguments";
                Console.WriteLine("mark-in-progress: {0}", msg);
                return;
            }
            break;
        case "mark-done":
            if (argCount < 1 || argCount > 1)
            {
                var msg = argCount < 2 ? "Please provide an ID" : "Too many arguments";
                Console.WriteLine("mark-done: {0}", msg);
                return;
            }

            break;
        case "list":
            if (argCount > 1)
            {
                Console.WriteLine("list: Too many arguments");
                return;
            }
            break;
        default:
            Console.WriteLine("Unknown command");
            return;
    }
}

static bool isNotNumber(string command, string arg)
{
    if (!int.TryParse(arg, out _))
    {
        Console.WriteLine($"{command}: parameter must be a number");
        return true;
    }
    return false;
}

