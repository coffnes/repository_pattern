using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RepoTask.DataAccessLayer;

public class MinusMongoClient
{
    public IMongoClient MongoClient;
    public MinusMongoClient(IOptions<MinusMongoDatabaseSettings> databaseSettings)
    {
        MongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
    }
}