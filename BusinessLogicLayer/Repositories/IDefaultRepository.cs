using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer.Repositories;

public interface IDefaultRepository<T> : IMongoRepository<T>
{
}