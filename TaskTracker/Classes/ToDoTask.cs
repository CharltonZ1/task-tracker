using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskTracker.Classes;

// called ToDoTask to avoid conflict with System.Threading.Tasks.Task
public class ToDoTask
{
    [JsonConstructor]
    public ToDoTask(int id, string description, StatusEnum status, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public ToDoTask(string description)
    {
        Description = description;
        Status = StatusEnum.Todo;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public int Id { get; set; }
    public string Description { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public void Update(string description, StatusEnum status)
    {
        Description = description;
        Status = status;
        UpdatedAt = DateTime.Now;
    }

    public override string ToString()
    {
        return $"ID: {Id}\nDescription: {Description}\nStatus: {Status}\nCreated At: {CreatedAt}\nUpdated At: {UpdatedAt}";
    }
}

public enum StatusEnum
{
    Todo,
    InProgress,
    Done
}