using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer.Repositories;

public class PlusMongoRepository : IPlusRepository<string>
{
    private readonly IMongoCollection<Entity<string>> _plusTemperatureCollection;
    public PlusMongoRepository(MongoClient mongoClient, IOptions<WeatherDatabaseSettings> weatherDatabaseSettings)
    {
        var mongoDatabse = mongoClient.GetDatabase(weatherDatabaseSettings.Value.DatabaseName);
        _plusTemperatureCollection = mongoDatabse.GetCollection<Entity<string>>(weatherDatabaseSettings.Value.PlusTemperatureCollectionName);
    }
    public async Task AddAsync(Entity<string> entity)
    {
        await _plusTemperatureCollection.InsertOneAsync(entity);
    }

    public async Task AddChunkAsync(IList<Entity<string>> entities)
    {
        await _plusTemperatureCollection.InsertManyAsync(entities);
    }
}