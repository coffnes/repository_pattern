namespace RepoTask.DataAccessLayer.Repositories;

public class MongoRepositoryManager
{
    private readonly IEnumerable<IMongoRepository<string>> _repositories;
    private readonly IDefaultRepository<string> _defaultRepository;

    public MongoRepositoryManager(IEnumerable<IMongoRepository<string>> repositories, IDefaultRepository<string> defaultRepository)
    {
        _repositories = repositories;
        _defaultRepository = defaultRepository;
    }

    public IList<TemperatureEntity<string>> GetAll()
    {
        List<TemperatureEntity<string>> result = new();
        foreach(var r in _repositories)
        {
            result = result.Union(r.GetAll()).ToList();
        }
        return result;
    }

    public IList<TemperatureEntity<string>> GetByCity(string? city)
    {
        List<TemperatureEntity<string>> result = new();
        foreach(var r in _repositories)
        {
            result = result.Union(r.GetByCity(city)).ToList();
        }
        return result;
    }

    public IList<TemperatureEntity<string>> GetByDate(DateOnly dateFrom, DateOnly dateTo)
    {
        List<TemperatureEntity<string>> result = new();
        foreach(var r in _repositories)
        {
            result = result.Union(r.GetByDate(dateFrom, dateTo)).ToList();
        }
        return result;
    }
    public async Task DeleteAll()
    {
        foreach(var r in _repositories)
        {
            await r.DeleteAll();
        }
    }
    public IList<TemperatureEntity<string>> GetOnlyZeroTemperature()
    {
        return _defaultRepository.GetAll().ToList();
    }
}