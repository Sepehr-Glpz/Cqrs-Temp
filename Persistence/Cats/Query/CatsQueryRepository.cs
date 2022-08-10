using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SGSX.CqrsTemp.Application.CatsFeatures.Query.DTOs;
using SGSX.CqrsTemp.Domain.Models;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Persistence.Base;

namespace SGSX.CqrsTemp.Persistence.Cats.Query;
internal class CatsQueryRepository : MongoQueryRepositoryBase<Cat>, ICatsQueryRepository
{
    public CatsQueryRepository(IMongoClient mongoClient, ILogger<CatsQueryRepository> logger, MongoRepositoryConfig repoConfig) : base(mongoClient, logger, repoConfig)
    {
    }

    private const string CASE_INSENSETIVE_COLLACTION = "en";
    private static readonly Collation _caseInsensetiveCollation = new Collation(CASE_INSENSETIVE_COLLACTION, strength: CollationStrength.Primary);
    public MetaResult<Cat> GetByName(string name)
    {
        try
        {
            var findOptions = new FindOptions()
            {
                Collation = _caseInsensetiveCollation,
            };

            var searchFilter = Builders<Cat>.Filter.Eq(c => c.Name, name);

            var cat = Entities.Find(searchFilter, findOptions).FirstOrDefault();

            if(cat is default(Cat))
            {
                return MetaResult<Cat>.CreateFailed().WithValidation(Validation.FromApplicationError("No record found!"));
            }
            else
            {
                return MetaResult<Cat>.CreateSuccessful().WithValue(cat);
            }
        }
        catch(MongoException ex)
        {
            Logger.LogWarning(ex, ex.Message);
            return MetaResult<Cat>.CreateFailed().WithValidation(ex);
        }
    }

    public async Task<MetaResult<Cat>> GetByNameAsync(string name)
    {
        try
        {
            var findOptions = new FindOptions()
            {
                Collation = _caseInsensetiveCollation,
            };

            var searchFilter = Builders<Cat>.Filter.Eq(c => c.Name, name);

            var cat = await Entities.Find(searchFilter, findOptions).FirstOrDefaultAsync();

            if (cat is default(Cat))
            {
                return MetaResult<Cat>.CreateFailed().WithValidation(Validation.FromApplicationError("No record found!"));
            }
            else
            {
                return MetaResult<Cat>.CreateSuccessful().WithValue(cat);
            }
        }
        catch (MongoException ex)
        {
            Logger.LogWarning(ex, ex.Message);
            return MetaResult<Cat>.CreateFailed().WithValidation(ex);
        }
    }

    public Result CreateCat(Cat cat)
    {
        try
        {
            Entities.InsertOne(cat);
            return Result.BasicSuccess;
        }
        catch(MongoException ex)
        {
            return Result.CreateFailed().WithValidation(ex);
        }
    }

    public async Task<Result> CreateCatAsync(Cat cat)
    {
        try
        {
            await Entities.InsertOneAsync(cat);
            return Result.BasicSuccess;
        }
        catch (MongoException ex)
        {
            return Result.CreateFailed().WithValidation(ex);
        }
    }

    public MetaResult<CatBasicInfo> GetBasicInfoById(Guid id)
    {
        var partialQuery = Builders<Cat>.Projection
            .Include(c => c.Id)
            .Include(c => c.Description)
            .Include(c => c.Name)
            .Include(c => c.CatBreed)
            .Include(c => c.CreateDate);
        var filter = Builders<Cat>.Filter.Eq(c => c.Id, id);

        var cat = Entities.Find(filter).Project(partialQuery).As<Cat>().FirstOrDefault();

        if (cat is default(Cat))
        {
            return MetaResult<CatBasicInfo>.CreateFailed().WithValidation(Validation.FromApplicationError("No record found!"));
        }
        else
        {
            return MetaResult<CatBasicInfo>.CreateSuccessful().WithValue(MapCatToBasicInfo(cat));
        }
    }

    public async Task<MetaResult<CatBasicInfo>> GetBasicInfoByIdAsync(Guid id)
    {
        var partialQuery = Builders<Cat>.Projection
            .Include(c => c.Id)
            .Include(c => c.Description)
            .Include(c => c.Name)
            .Include(c => c.CatBreed)
            .Include(c => c.CreateDate);
        var filter = Builders<Cat>.Filter.Eq(c => c.Id, id);

        var cat = await Entities.Find(filter).Project(partialQuery).As<Cat>().FirstOrDefaultAsync();

        if (cat is default(Cat))
        {
            return MetaResult<CatBasicInfo>.CreateFailed().WithValidation(Validation.FromApplicationError("No record found!"));
        }
        else
        {
            return MetaResult<CatBasicInfo>.CreateSuccessful().WithValue(MapCatToBasicInfo(cat));
        }
    }

    protected static CatBasicInfo MapCatToBasicInfo(Cat model)
    {
        var newModel = new CatBasicInfo()
        {
            Id = model.Id,
            Description = model.Description,
            CreateDate = model.CreateDate,
            CatBreed = model.CatBreed,
            Name = model.Name,
        };
        return newModel;
    }

    protected override void MapEntity(BsonClassMap<Cat> classMap)
    {
        classMap.MapIdMember(c => c.Id);
        classMap.MapCreator(c => new Cat(c.Id, c.CreateDate, c.MouseBuddyId, c.MouseBuddy, c.Name, c.Description, c.CatBreed));
        classMap.MapMember(c => c.Name);
        classMap.MapMember(c => c.Description);
        classMap.MapMember(c => c.CreateDate);
        classMap.MapMember(c => c.CatBreed);
        
        BsonClassMap.RegisterClassMap<MouseBuddy>(cm =>
        {
            cm.MapIdField(c => c.Id);
            cm.MapMember(c => c.CreateDate);
            cm.MapMember(c => c.AttackPower);
            cm.MapMember(c => c.Name);
        });

        classMap.MapMember(c => c.MouseBuddy);
    }
}

