using MongoDB.Driver;
using Microsoft.Extensions.Options;
using RepoTask.DataAccessLayer;

namespace RepoTask.BusinessLogicLayer;

public class WeatherDataAccessor
{
    private readonly IMongoCollection<WeatherForecast> _plusTemperatureCollection;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IOptions<WeatherDatabaseSettings> _weatherDatabaseSettings;
    public WeatherDataAccessor(MongoClient mongoClient, IOptions<WeatherDatabaseSettings> weatherDatabaseSettings)
    {
        _weatherDatabaseSettings = weatherDatabaseSettings;
        _mongoDatabase = mongoClient.GetDatabase(weatherDatabaseSettings.Value.DatabaseName);
        _plusTemperatureCollection = _mongoDatabase.GetCollection<WeatherForecast>(weatherDatabaseSettings.Value.PlusTemperatureCollectionName);
    }
    public IEnumerable<WeatherForecast> GetAll()
    {
        var result = _plusTemperatureCollection.Aggregate()
            .UnionWith(_mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.MinusTemperatureCollectionName))
            .UnionWith(_mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.ZeroTemperatureCollectionName))
            .ToList();
        return result;
    }

    public IList<WeatherForecast> GetByCity(string? city)
    {
        //Получить коллекцию объединением 3 коллекций
        var filter = Builders<WeatherForecast>
            .Filter
            .Eq(m => m.City, city);
        var pipeline = PipelineStageDefinitionBuilder.Match(filter);
        var pipelines = PipelineDefinition<WeatherForecast, WeatherForecast>
            .Create(new PipelineStageDefinition<WeatherForecast, WeatherForecast>[]{ pipeline });
        var result = _plusTemperatureCollection.Aggregate()
            .Match(filter)
            .UnionWith(_mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.MinusTemperatureCollectionName), pipelines)
            .UnionWith(_mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.ZeroTemperatureCollectionName), pipelines)
            .ToList();
        return result;
    }

    public IList<WeatherForecast> GetByDate(DateOnly dateFrom, DateOnly dateTo)
    {
        var filter = Builders<WeatherForecast>
            .Filter
            .And(Builders<WeatherForecast>.Filter.Gt(m => m.Date, dateFrom), Builders<WeatherForecast>.Filter.Lt(m => m.Date, dateTo));
        var pipeline = PipelineStageDefinitionBuilder.Match(filter);
        var pipelines = PipelineDefinition<WeatherForecast, WeatherForecast>
            .Create(new PipelineStageDefinition<WeatherForecast, WeatherForecast>[]{ pipeline });
        var result = _plusTemperatureCollection.Aggregate()
            .Match(filter)
            .UnionWith(_mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.MinusTemperatureCollectionName), pipelines)
            .UnionWith(_mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.ZeroTemperatureCollectionName), pipelines)
            .ToList();
        return result;
    }

    public IList<WeatherForecast> GetOnlyZeroTemperature()
    {
        var zeroTemperatureCollection = _mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.ZeroTemperatureCollectionName);
        return zeroTemperatureCollection.Aggregate().ToList();
    }

    public async Task DeleteAll()
    {
        await _mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.PlusTemperatureCollectionName).DeleteManyAsync(Builders<WeatherForecast>.Filter.Empty);
        await _mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.MinusTemperatureCollectionName).DeleteManyAsync(Builders<WeatherForecast>.Filter.Empty);
        await _mongoDatabase.GetCollection<WeatherForecast>(_weatherDatabaseSettings.Value.ZeroTemperatureCollectionName).DeleteManyAsync(Builders<WeatherForecast>.Filter.Empty);
    }
}