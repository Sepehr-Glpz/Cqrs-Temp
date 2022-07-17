using SGSX.CqrsTemp.Application.CatsFeatures.Query.DTOs;
using SGSX.CqrsTemp.Domain.Models;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Persistence.Base;

namespace SGSX.CqrsTemp.Persistence.Cats.Query;
public interface ICatsQueryRepository : IQueryRepository<Cat>
{
    Result CreateCat(Cat cat);
    Task<Result> CreateCatAsync(Cat cat);

    MetaResult<CatBasicInfo> GetBasicInfoById(Guid id);
    Task<MetaResult<CatBasicInfo>> GetBasicInfoByIdAsync(Guid id);

    MetaResult<Cat> GetByName(string name);
    Task<MetaResult<Cat>> GetByNameAsync(string name);
}

