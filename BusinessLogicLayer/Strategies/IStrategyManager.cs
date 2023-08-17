namespace RepoTask.BusinessLogicLayer.Strategies;

public interface IStrategyManager<K>
{
    public IStrategy<K> GetStrategy(K resolveParam);
}