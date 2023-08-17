namespace RepoTask.Models;

public class WeatherDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ZeroTemperatureCollectionName { get; set; } = null!;
    public string PlusTemperatureCollectionName { get; set; } = null!;
    public string MinusTemperatureCollectionName { get; set; } = null!;
}