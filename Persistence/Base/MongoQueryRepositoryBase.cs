using SGSX.CqrsTemp.Domain.Base;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;

namespace SGSX.CqrsTemp.Persistence.Base;
internal abstract class MongoQueryRepositoryBase<TEntity> : object where TEntity : BaseEntity
{
    protected IMongoClient MongoClient { get; }
    protected IMongoCollection<TEntity>? EntiryCollection { get; private set; }
    protected ILogger<MongoQueryRepositoryBase<TEntity>> Logger { get; }
    public MongoQueryRepositoryBase(IMongoClient mongoClient, ILogger<MongoQueryRepositoryBase<TEntity>> logger, MongoRepositoryConfig repoConfig, MongoEntityConfig entityConfig) : base()
    {
        MongoClient = mongoClient;
        Logger = logger;

        var classMap = new BsonClassMap<TEntity>();
        MapEntity(classMap);
        BsonClassMap.RegisterClassMap(classMap);
    }

    private void Connect(string[] connectionStrings, string databaseName)
    {
        var db = MongoClient.GetDatabase(databaseName);
    }

    protected abstract void CreateCollection();

    protected abstract void MapEntity(BsonClassMap<TEntity> classMap);

}

