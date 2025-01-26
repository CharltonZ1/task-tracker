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

    public override string ToString()
    {
        return $"ID: {Id}\nDescription: {Description}\nStatus: {Status}\nCreated At: {CreatedAt}\nUpdated At: {UpdatedAt}";
    }

    public static List<ToDoTask> ReadAll()
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

    public static void WriteAll(List<ToDoTask> tasks)
    {
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText("data.json", json);
    }


    private void UpdatedAtNow()
    {
        _updatedAt = DateTime.Now;
    }
}

public enum StatusEnum
{
    Todo,
    InProgress,
    Done
}