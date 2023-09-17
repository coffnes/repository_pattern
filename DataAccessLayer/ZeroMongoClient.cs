using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RepoTask.DataAccessLayer;

public class ZeroMongoClient
{
    public IMongoClient MongoClient;
    public ZeroMongoClient(IOptions<ZeroMongoDatabaseSettings> databaseSettings)
    {
        MongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
    }
}