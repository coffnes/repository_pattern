using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer.Repositories;

public interface IMinusRepository<T> : IMongoRepository<T>
{
}