namespace RepoTask.DataAccessLayer;

public class ZeroMongoDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ZeroTemperatureCollectionName { get; set; } = null!;
}