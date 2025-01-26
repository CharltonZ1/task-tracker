namespace TaskTracker.Classes;

public class Task
{
    private static int _id = 0;
    public int Id { get; set; }
    public string Description { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Task(string desc)
    {
        Id = _id++;
        Description = desc;
        Status = StatusEnum.todo;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}

public enum StatusEnum
{
    todo,
    inProgress,
    done
}