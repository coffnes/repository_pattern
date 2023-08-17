using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer.Repositories;

public class MinusMongoRepository : IMinusRepository<string>
{
    private readonly IMongoCollection<Entity<string>> _minusTemperatureCollection;
    public MinusMongoRepository(MongoClient mongoClient, IOptions<WeatherDatabaseSettings> weatherDatabaseSettings)
    {
        var mongoDatabse = mongoClient.GetDatabase(weatherDatabaseSettings.Value.DatabaseName);
        _minusTemperatureCollection = mongoDatabse.GetCollection<Entity<string>>(weatherDatabaseSettings.Value.MinusTemperatureCollectionName);
    }
    public async Task CreateAsync(Entity<string> entity)
    {
        await _minusTemperatureCollection.InsertOneAsync(entity);
    }
}