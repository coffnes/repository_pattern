using RepoTask.DataAccessLayer;

namespace RepoTask.DataAccessLayer.Repositories;

public interface IMongoRepository<T> : IRepository<T>
{
    public Task AddAsync(Entity<T> entity);

    public Task AddChunkAsync(IList<Entity<T>> entities);
}