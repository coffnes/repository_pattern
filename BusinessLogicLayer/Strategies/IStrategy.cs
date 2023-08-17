namespace RepoTask.BusinessLogicLayer.Strategies;

public interface IStrategy<T>
{
    public bool Algorithm(T resolveParam);
}