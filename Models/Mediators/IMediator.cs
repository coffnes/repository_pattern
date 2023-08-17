namespace RepoTask.Models;

public interface IMediator<T, K>
{
    public IMongoRepository<T> GetRepository();
    public bool StrategyTypeCheck(IStrategy<K> strategy);
}