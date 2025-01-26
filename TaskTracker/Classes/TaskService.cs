namespace TaskTracker.Classes;

internal class TaskService
{
    private readonly TaskManager _taskManager;

    public TaskService(TaskManager taskManager)
    {
        _taskManager = taskManager;
    }

    public void AddTask(string description)
    {
        var task = new ToDoTask(description);
        _taskManager.AddTask(task);
        Console.WriteLine($"Task added successfully (ID: {task.Id})");
    }

    public void UpdateTask(int id, string description)
    {
        if (!_taskManager.UpdateTask(id, description))
        {
            Console.WriteLine("Task not found");
            return;
        }
        Console.WriteLine("Task updated successfully");
    }

    public void DeleteTask(int id)
    {
        if (!_taskManager.DeleteTask(id))
        {
            Console.WriteLine("Task not found");
            return;
        }
        Console.WriteLine("Task deleted successfully");
    }

    public void MarkInProgress(int id)
    {
        if (!_taskManager.MarkInProgress(id))
        {
            Console.WriteLine("Task not found");
            return;
        }
        Console.WriteLine("Task marked as in progress");
    }

    public void MarkDone(int id)
    {
        if (!_taskManager.MarkDone(id))
        {
            Console.WriteLine("Task not found");
            return;
        }
        Console.WriteLine("Task marked as done");
    }

    public void ListTasks(string? statusString = null)
    {
        var status = ParseStatus(statusString);
        if (status == null)
        {
            return;
        }

        List<ToDoTask> tasks = _taskManager.LoadTasks();

        if (status == null)
        {
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
        }
        else
        {
            foreach (var task in tasks)
            {
                if (task.Status == status)
                {
                    Console.WriteLine(task);
                }
            }
        }
    }

    private StatusEnum? ParseStatus(string? statusString)
    {
        if (statusString == null)
        {
            return null;
        }

        if (Enum.TryParse<StatusEnum>(statusString.Replace("_", ""), true, out var status))
        {
            return status;
        }
        else
        {
            return null;
        }
    }
}
