using RepoTask.BusinessLogicLayer.Repositories;
using RepoTask.BusinessLogicLayer.Strategies;

namespace RepoTask.BusinessLogicLayer.Mediators;

public interface IMediatorManager<T, K>
{
    public IMongoRepository<T> GetCurrentRepository(K resolveParam);
}