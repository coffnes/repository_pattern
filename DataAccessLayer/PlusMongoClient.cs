using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RepoTask.DataAccessLayer;

public class PlusMongoClient
{
    public IMongoClient MongoClient;
    public PlusMongoClient(IOptions<PlusMongoDatabaseSettings> databaseSettings)
    {
        MongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
    }
}