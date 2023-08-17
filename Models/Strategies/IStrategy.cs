namespace RepoTask.Models;

public interface IStrategy<T>
{
    public bool Algorithm(T resolveParam);
}