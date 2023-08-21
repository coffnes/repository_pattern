using RepoTask.DataAccessLayer.Repositories;

namespace RepoTask.BusinessLogicLayer.Mediators;

public interface IMediatorManager<T, K>
{
    public IMongoRepository<T> GetCurrentRepository(K resolveParam);
}