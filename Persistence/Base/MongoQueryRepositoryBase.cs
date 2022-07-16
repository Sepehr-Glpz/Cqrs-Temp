using SGSX.CqrsTemp.Domain.Base;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using SGSX.CqrsTemp.Domain.Results;

namespace SGSX.CqrsTemp.Persistence.Base;
internal abstract class MongoQueryRepositoryBase<TEntity> : object, IQueryRepository<TEntity> where TEntity : BaseEntity
{
    protected IMongoClient MongoClient { get; }
    protected IMongoDatabase MongoDatabase { get; }
    protected IMongoCollection<TEntity> Entities { get; }
    protected ILogger<MongoQueryRepositoryBase<TEntity>> Logger { get; }
    public MongoQueryRepositoryBase(IMongoClient mongoClient, ILogger<MongoQueryRepositoryBase<TEntity>> logger, MongoRepositoryConfig repoConfig) : base()
    {
        MongoClient = mongoClient;
        Logger = logger;
        var classMap = new BsonClassMap<TEntity>();
        MapEntity(classMap);
        BsonClassMap.RegisterClassMap(classMap);
        MongoDatabase = MongoClient.GetDatabase(repoConfig.DatabaseName);
        Entities = MongoDatabase.GetCollection<TEntity>(repoConfig.EntityName);
    }

    protected abstract void MapEntity(BsonClassMap<TEntity> classMap);

    public virtual MetaResult<TEntity> GetById(Guid id)
    {
        var entity = Entities.Find(c => c.Id == id).SingleOrDefault();
        if(entity is default(TEntity))
        {
            return MetaResult<TEntity>.CreateFailed().WithValidation(Validation.FromApplicationError("No result found!"));
        }
        else
        {
            return MetaResult<TEntity>.CreateSuccessful().WithValue(entity);
        }
    }

    public virtual async Task<MetaResult<TEntity>> GetByIdAsync(Guid id)
    {
        var entity = await Entities.Find(c => c.Id == id).SingleOrDefaultAsync();
        if (entity is default(TEntity))
        {
            return MetaResult<TEntity>.CreateFailed().WithValidation(Validation.FromApplicationError("No result found!"));
        }
        else
        {
            return MetaResult<TEntity>.CreateSuccessful().WithValue(entity);
        }
    }

    public virtual MetaResult<IReadOnlyList<TEntity>> GetAll()
    {
        var entities = Entities.Find(Builders<TEntity>.Filter.Empty).ToList();
        if(entities is null || entities.Count == 0)
        {
            return MetaResult<IReadOnlyList<TEntity>>.CreateFailed()
                .WithValidation(Validation.FromApplicationError("no records found!"));
        }
        else
        {
            return MetaResult<IReadOnlyList<TEntity>>.CreateSuccessful()
                .WithValue(entities);
        }
    }

    public virtual async Task<MetaResult<IReadOnlyList<TEntity>>> GetAllAsync()
    {
        var entities = await Entities.Find(Builders<TEntity>.Filter.Empty).ToListAsync();
        if (entities is null || entities.Count == 0)
        {
            return MetaResult<IReadOnlyList<TEntity>>.CreateFailed()
                .WithValidation(Validation.FromApplicationError("no records found!"));
        }
        else
        {
            return MetaResult<IReadOnlyList<TEntity>>.CreateSuccessful()
                .WithValue(entities);
        }
    }
}

