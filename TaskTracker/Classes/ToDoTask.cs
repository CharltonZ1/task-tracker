using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskTracker.Classes;

// called ToDoTask to avoid conflict with System.Threading.Tasks.Task
public class ToDoTask
{
    private static int _id = 1;
    private string _description = string.Empty;
    private DateTime _updatedAt;
    private StatusEnum _status = StatusEnum.Todo;

    public int Id { get; }
    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Description cannot be null or empty");
            _description = value;
            UpdatedAtNow();
        }
    }
    public StatusEnum Status
    {
        get => _status;
        set
        {
            _status = value;
            UpdatedAtNow();
            return;
        }
    }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime UpdatedAt
    {
        get => _updatedAt;
        set
        {
            _updatedAt = value;
            return;
        }
    }

    public ToDoTask(string desc)
    {
        Id = _id++;
        Description = desc;
        CreatedAt = DateTime.Now;
    }

    [JsonConstructor]
    public ToDoTask(int id, string description, DateTime createdAt, DateTime updatedAt, StatusEnum status)
    {
        Id = id;
        _description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Status = status;
    }

    private void UpdatedAtNow()
    {
        _updatedAt = DateTime.Now;
    }

    public override string ToString()
    {
        return $"\nID: {Id}\nDescription: {Description}\nStatus: {Status}\nCreated At: {CreatedAt}\nUpdated At: {UpdatedAt}";
    }

    public static void AddTask(string desc)
    {
        var tasks = ReadAll();
        tasks.Add(new ToDoTask(desc));
        WriteAll(tasks);
        Console.WriteLine("Task added successfully");
    }

    public static void UpdateTask(int id, string desc)
    {
        var tasks = ReadAll();
        var task = GetIfExists(tasks, id);
        if (task == null) return;
        task.Description = desc;
        task.UpdatedAtNow();
        WriteAll(tasks);
        Console.WriteLine("Task updated successfully");
    }

    public static void DeleteTask(int id)
    {
        var tasks = ReadAll();
        var task = GetIfExists(tasks, id);
        if (task == null) return;
        tasks.Remove(task);
        WriteAll(tasks);
        Console.WriteLine("Task deleted successfully");
    }

    public static void MarkInProgress(int id)
    {
        var tasks = ReadAll();
        var task = GetIfExists(tasks, id);
        if (task == null) return;
        task.Status = StatusEnum.InProgress;
        task.UpdatedAtNow();
        WriteAll(tasks);
        Console.WriteLine("Task marked as in progress");
    }

    public static void MarkDone(int id)
    {
        var tasks = ReadAll();
        var task = GetIfExists(tasks, id);
        if (task == null) return;
        task.Status = StatusEnum.Done;
        task.UpdatedAtNow();
        WriteAll(tasks);
        Console.WriteLine("Task marked as done");
    }

    public static void ListTasks(StatusEnum? status = null)
    {
        var tasks = ReadAll();
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

    private static ToDoTask? GetIfExists(List<ToDoTask> tasks, int id)
    {
        var task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine("Task not found");
            return null;
        }
        return task;
    }

    private static List<ToDoTask> ReadAll()
    {
        if (!File.Exists("data.json"))
        {
            File.WriteAllText("data.json", "[]");
        }
        var json = File.ReadAllText("data.json");
        var tasks = JsonSerializer.Deserialize<List<ToDoTask>>(json) ?? [];

        _id = tasks.Count + 1;
        return tasks;
    }

    private static void WriteAll(List<ToDoTask> tasks)
    {
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText("data.json", json);
    }
}

public enum StatusEnum
{
    Todo,
    InProgress,
    Done
}