using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer.Repositories;

public interface IMongoRepository<T> : IRepository<T>
{
    public Task CreateAsync(Entity<T> entity);
}