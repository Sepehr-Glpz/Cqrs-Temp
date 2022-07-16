using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SGSX.CqrsTemp.Domain.Models;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Persistence.Base;

namespace SGSX.CqrsTemp.Persistence.Cats.Query;
internal class CatsQueryRepository : MongoQueryRepositoryBase<Cat>, ICatsQueryRepository
{
    public CatsQueryRepository(IMongoClient mongoClient, ILogger<MongoQueryRepositoryBase<Cat>> logger, MongoRepositoryConfig repoConfig) : base(mongoClient, logger, repoConfig)
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

    protected override void MapEntity(BsonClassMap<Cat> classMap)
    {
        classMap.MapCreator(c => new Cat(c.Id, c.CreateDate, c.MouseBuddyId, c.MouseBuddy, c.Name, c.Description, c.CatBreed));
    }
}

