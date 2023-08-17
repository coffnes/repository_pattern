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
    public async Task CreateAsync(Entity<string> entity)
    {
        await _plusTemperatureCollection.InsertOneAsync(entity);
    }
}