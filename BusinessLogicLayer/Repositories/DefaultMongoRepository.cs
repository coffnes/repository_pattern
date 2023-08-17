using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer.Repositories;

public class DefaultMongoRepository : IDefaultRepository<string>
{
    private readonly IMongoCollection<Entity<string>> _zeroTemperatureCollection;
    public DefaultMongoRepository(MongoClient mongoClient, IOptions<WeatherDatabaseSettings> weatherDatabaseSettings)
    {
        var mongoDatabse = mongoClient.GetDatabase(weatherDatabaseSettings.Value.DatabaseName);
        _zeroTemperatureCollection = mongoDatabse.GetCollection<Entity<string>>(weatherDatabaseSettings.Value.ZeroTemperatureCollectionName);
    }
    public async Task CreateAsync(Entity<string> entity)
    {
        await _zeroTemperatureCollection.InsertOneAsync(entity);
    }
}