using RepoTask.DataAccessLayer;

namespace RepoTask.DataAccessLayer.Repositories;

public interface IDefaultRepository<T> : IMongoRepository<T>
{
}