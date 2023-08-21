namespace RepoTask.DataAccessLayer;

public class MinusMongoDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string MinusTemperatureCollectionName { get; set; } = null!;
}