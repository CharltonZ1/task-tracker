using TaskTracker.Classes;

var taskManager = new TaskManager("data.json");
var taskService = new TaskService(taskManager);
var commandHandler = new CommandHandler(taskService);

if (args.Length == 0)
{
    Console.WriteLine("Usage: task-tracker <command> [args]");
    return;
}

try
{
    commandHandler.HandleCommand(args);
}
catch (Exception e)
{
    Console.WriteLine($"An error occurred: {e.Message}");
}
