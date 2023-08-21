namespace RepoTask.DataAccessLayer;

public abstract class Entity<T>
{
    public T Id { get; set; }
}
