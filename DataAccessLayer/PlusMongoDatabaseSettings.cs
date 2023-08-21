namespace RepoTask.DataAccessLayer;

public class PlusMongoDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string PlusTemperatureCollectionName { get; set; } = null!;
}