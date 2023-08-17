namespace RepoTask.Models;

public abstract class TemperatureEntity<T> : Entity<T>
{
    public int TemperatureC { get; set; }
}