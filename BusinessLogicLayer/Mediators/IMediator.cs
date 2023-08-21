using RepoTask.DataAccessLayer.Repositories;
using RepoTask.BusinessLogicLayer.Strategies;

namespace RepoTask.BusinessLogicLayer.Mediators;

public interface IMediator<T, K>
{
    public IMongoRepository<T> GetRepository();
    public bool StrategyTypeCheck(IStrategy<K> strategy);
}