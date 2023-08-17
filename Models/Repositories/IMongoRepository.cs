namespace RepoTask.Models;

public interface IMongoRepository<T> : IRepository<T>
{
    public Task CreateAsync(Entity<T> entity);
}