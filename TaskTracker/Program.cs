using TaskTracker.Classes;

var tasks = ToDoTask.ReadAll();

if (args.Length == 0)
{
    Console.WriteLine("Please provide a command");
    return;
}

var command = args[0];
var argCount = args.Length - 1;

handleCommandValidation(command, argCount);

switch (command.ToLower())
{
    case "add":
        addTask(tasks, args[1]);
        break;
    case "update":
        updateTask(tasks, int.Parse(args[1]), args[2]);
        break;
    case "delete":
        deleteTask(tasks, int.Parse(args[1]));
        break;
    case "mark-in-progress":
        markInProgress(tasks, int.Parse(args[1]));
        break;
    case "mark-done":
        markDone(tasks, int.Parse(args[1]));
        break;
    case "list":
        if (argCount == 0)
        {
            listTasks(tasks);
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

            listTasks(tasks, status);
        }
        break;
    default:
        Console.WriteLine("Unknown command");
        break;
}

static void addTask(List<ToDoTask> tasks, string desc)
{
    var newTask = new ToDoTask(desc);
    tasks.Add(newTask);
    ToDoTask.WriteAll(tasks);
    Console.WriteLine("Task added successfully (ID: {0})", newTask.Id);
}

static void updateTask(List<ToDoTask> tasks, int id, string desc)
{
    var task = getIfExists(tasks, id);
    if (task == null) return;
    task.Description = desc;
    ToDoTask.WriteAll(tasks);
    Console.WriteLine("Task updated successfully");
}

static void deleteTask(List<ToDoTask> tasks, int id)
{
    var task = getIfExists(tasks, id);
    if (task == null) return;
    tasks.Remove(task);
    ToDoTask.WriteAll(tasks);
    Console.WriteLine("Task deleted successfully");
}

static void markInProgress(List<ToDoTask> tasks, int id)
{
    var task = getIfExists(tasks, id);
    if (task == null) return;
    task.Status = StatusEnum.InProgress;
    ToDoTask.WriteAll(tasks);
    Console.WriteLine("Task marked as in progress");
}

static void markDone(List<ToDoTask> tasks, int id)
{
    var task = getIfExists(tasks, id);
    if (task == null) return;
    task.Status = StatusEnum.Done;
    ToDoTask.WriteAll(tasks);
    Console.WriteLine("Task marked as done");
}

static void listTasks(List<ToDoTask> tasks, StatusEnum? status = null)
{
    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks to list");
        return;
    }
    if (status == null)
    {
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }
    else
    {
        foreach (var task in tasks.FindAll(t => t.Status == status))
        {
            Console.WriteLine(task);
        }
    }
}

static ToDoTask? getIfExists(List<ToDoTask> tasks, int id)
{
    var task = tasks.Find(t => t.Id == id);
    if (task == null)
    {
        Console.WriteLine("Task not found");
        return null;
    }
    return task;
}


static void handleCommandValidation(string command, int argCount)
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
            break;
        case "update":
            if (argCount < 2 || argCount > 2)
            {
                var msg = argCount < 2 ? "Please provide an ID and a description" : "Too many arguments";
                Console.WriteLine("update: {0}", msg);
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

