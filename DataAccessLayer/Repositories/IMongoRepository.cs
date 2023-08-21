namespace RepoTask.DataAccessLayer.Repositories;

public interface IMongoRepository<T> : IRepository<T>
{
    public Task AddAsync(TemperatureEntity<T> entity);

    public Task AddChunkAsync(IList<TemperatureEntity<T>> entities);

    public IList<TemperatureEntity<T>> GetByCity(string? city);
    public IList<TemperatureEntity<T>> GetByDate(DateOnly dateFrom, DateOnly dateTo);
    public Task DeleteAll();
    public IEnumerable<TemperatureEntity<T>> GetAll();
}