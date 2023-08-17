namespace RepoTask.Models;

public interface IMediatorManager<T, K>
{
    public IMongoRepository<T> GetCurrentRepository(K resolveParam);
}