using RepoTask.DataAccessLayer;

namespace RepoTask.DataAccessLayer.Repositories;

public interface IMinusRepository<T> : IMongoRepository<T>
{
}