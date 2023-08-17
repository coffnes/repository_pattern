namespace RepoTask.Models;

public interface IStrategyManager<K>
{
    public IStrategy<K> GetStrategy(K resolveParam);
}