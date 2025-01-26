namespace TaskTracker.Classes;

internal class CommandHandler
{
    private readonly TaskService _taskService;
    private readonly Dictionary<string, Func<string[], bool>> _commands;

    public CommandHandler(TaskService taskService)
    {
        _taskService = taskService;
        _commands = new Dictionary<string, Func<string[], bool>>
        {
            { "add", HandleAdd },
            { "update", HandleUpdate },
            { "delete", HandleDelete },
            { "mark-in-progress", HandleMarkInProgress },
            { "mark-done", HandleMarkDone },
            { "list", HandleList }
        };
    }

    public void HandleCommand(string[] args)
    {
        var command = args[0].ToLower();

        if (!_commands.ContainsKey(command))
        {
            Console.WriteLine("Unknown command");
            return;
        }

        if (!_commands[command](args))
        {
            Console.WriteLine($"{command}: Invalid usage");
        }
    }

    private bool HandleAdd(string[] args)
    {
        if (args.Length != 2)
        {
            return false;
        }
        _taskService.AddTask(args[1]);
        return true;
    }

    private bool HandleUpdate(string[] args)
    {
        if (args.Length != 3 || !int.TryParse(args[1], out var id))
        {
            return false;
        }
        _taskService.UpdateTask(id, args[2]);
        return true;
    }

    private bool HandleDelete(string[] args)
    {
        if (args.Length != 2 || !int.TryParse(args[1], out var id))
        {
            return false;
        }
        _taskService.DeleteTask(id);
        return true;
    }

    private bool HandleMarkInProgress(string[] args)
    {
        if (args.Length != 2 || !int.TryParse(args[1], out var id))
        {
            return false;
        }
        _taskService.MarkInProgress(id);
        return true;
    }

    private bool HandleMarkDone(string[] args)
    {
        if (args.Length != 2 || !int.TryParse(args[1], out var id))
        {
            return false;
        }
        _taskService.MarkDone(id);
        return true;
    }

    private bool HandleList(string[] args)
    {
        if (args.Length == 1)
        {
            _taskService.ListTasks();
            return true;
        }
        if (args.Length != 2)
        {
            return false;
        }
        var status = args[1].Replace("-", "", StringComparison.OrdinalIgnoreCase);
        if (!Enum.TryParse<StatusEnum>(status, true, out var parsedStatus))
        {
            return false;
        }
        _taskService.ListTasks(parsedStatus.ToString());
        return true;
    }
}
