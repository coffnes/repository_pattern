using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RepoTask.DataAccessLayer;

namespace RepoTask.DataAccessLayer.Repositories;

public class MinusMongoRepository : IMinusRepository<string>
{
    private readonly IMongoCollection<TemperatureEntity<string>> _minusTemperatureCollection;
    public MinusMongoRepository(MinusMongoClient mongoClient, IOptions<MinusMongoDatabaseSettings> databaseSettings)
    {
        var mongoDatabase = mongoClient.MongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _minusTemperatureCollection = mongoDatabase.GetCollection<TemperatureEntity<string>>(databaseSettings.Value.MinusTemperatureCollectionName);
    }
    public async Task AddAsync(TemperatureEntity<string> entity)
    {
        await _minusTemperatureCollection.InsertOneAsync(entity);
    }
    public async Task AddChunkAsync(IList<TemperatureEntity<string>> entities)
    {
        await _minusTemperatureCollection.InsertManyAsync(entities);
    }
    public IList<TemperatureEntity<string>> GetByCity(string? city)
    {
        var filter = Builders<TemperatureEntity<string>>
            .Filter
            .Eq(m => m.City, city);
        var result = _minusTemperatureCollection.Aggregate()
            .Match(filter)
            .ToList();
        return result;
    }
    public IList<TemperatureEntity<string>> GetByDate(DateOnly dateFrom, DateOnly dateTo)
    {
        var filter = Builders<TemperatureEntity<string>>
            .Filter
            .And(Builders<TemperatureEntity<string>>.Filter.Gt(m => m.Date, dateFrom), Builders<TemperatureEntity<string>>.Filter.Lt(m => m.Date, dateTo));
        var result = _minusTemperatureCollection.Aggregate()
            .Match(filter)
            .ToList();
        return result;
    }
    public async Task DeleteAll()
    {
        await _minusTemperatureCollection.DeleteManyAsync(Builders<TemperatureEntity<string>>.Filter.Empty);
    }
    public IEnumerable<TemperatureEntity<string>> GetAll()
    {
        var result = _minusTemperatureCollection.Aggregate().ToList();
        return result;
    }
}