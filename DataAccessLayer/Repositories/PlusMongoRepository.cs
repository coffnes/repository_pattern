using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RepoTask.DataAccessLayer;

namespace RepoTask.DataAccessLayer.Repositories;

public class PlusMongoRepository : IPlusRepository<string>
{
    private readonly IMongoCollection<TemperatureEntity<string>> _plusTemperatureCollection;
    public PlusMongoRepository(MongoClient mongoClient, IOptions<WeatherDatabaseSettings> weatherDatabaseSettings)
    {
        var mongoDatabse = mongoClient.GetDatabase(weatherDatabaseSettings.Value.DatabaseName);
        _plusTemperatureCollection = mongoDatabse.GetCollection<TemperatureEntity<string>>(weatherDatabaseSettings.Value.PlusTemperatureCollectionName);
    }
    public async Task AddAsync(TemperatureEntity<string> entity)
    {
        await _plusTemperatureCollection.InsertOneAsync(entity);
    }

    public async Task AddChunkAsync(IList<TemperatureEntity<string>> entities)
    {
        await _plusTemperatureCollection.InsertManyAsync(entities);
    }
    public IList<TemperatureEntity<string>> GetByCity(string? city)
    {
        var filter = Builders<TemperatureEntity<string>>
            .Filter
            .Eq(m => m.City, city);
        var result = _plusTemperatureCollection.Aggregate()
            .Match(filter)
            .ToList();
        return result;
    }

    public IList<TemperatureEntity<string>> GetByDate(DateOnly dateFrom, DateOnly dateTo)
    {
        var filter = Builders<TemperatureEntity<string>>
            .Filter
            .And(Builders<TemperatureEntity<string>>.Filter.Gt(m => m.Date, dateFrom), Builders<TemperatureEntity<string>>.Filter.Lt(m => m.Date, dateTo));
        var result = _plusTemperatureCollection.Aggregate()
            .Match(filter)
            .ToList();
        return result;
    }

    public async Task DeleteAll()
    {
        await _plusTemperatureCollection.DeleteManyAsync(Builders<TemperatureEntity<string>>.Filter.Empty);
    }
    public IEnumerable<TemperatureEntity<string>> GetAll()
    {
        var result = _plusTemperatureCollection.Aggregate().ToList();
        return result;
    }
}