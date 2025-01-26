using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TaskTracker.Classes;

// Handles data persistence and retrieval
internal class TaskManager
{
    private readonly string _filePath;

    public TaskManager(string filePath)
    {
        _filePath = filePath;
    }

    public List<ToDoTask> LoadTasks()
    {
        // TODO: Handle exceptions
        if (!File.Exists(_filePath))
        {
            return [];
        }
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<ToDoTask>>(json) ?? [];
    }

    public void SaveTasks(List<ToDoTask> tasks)
    {
        // TODO: Handle exceptions
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(_filePath, json);
    }

    public void AddTask(ToDoTask task)
    {
        var tasks = LoadTasks();
        task.Id = GetNextId(tasks);
        tasks.Add(task);
        SaveTasks(tasks);
    }

    public bool UpdateTask(int id, string description)
    {
        var tasks = LoadTasks();
        var task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            return false;
        }
        task.Update(description, task.Status);
        SaveTasks(tasks);
        return true;
    }

    public bool DeleteTask(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            return false;
        }
        tasks.Remove(task);
        SaveTasks(tasks);
        return true;
    }

    public bool MarkInProgress(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            return false;
        }
        task.Update(task.Description, StatusEnum.InProgress);
        SaveTasks(tasks);
        return true;
    }

    public bool MarkDone(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            return false;
        }
        task.Update(task.Description, StatusEnum.Done);
        SaveTasks(tasks);
        return true;
    }

    public ToDoTask? GetTaskById(int id)
    {
        var tasks = LoadTasks();
        return tasks.Find(t => t.Id == id);
    }

    private static int GetNextId(List<ToDoTask> tasks)
    {
        return tasks.Count + 1;
    }
}
