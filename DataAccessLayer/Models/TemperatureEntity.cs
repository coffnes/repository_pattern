namespace RepoTask.DataAccessLayer;

public abstract class TemperatureEntity<T> : Entity<T>
{
    public int TemperatureC { get; set; }
    public DateOnly Date { get; set; }
    public string? City { get; set; }
}