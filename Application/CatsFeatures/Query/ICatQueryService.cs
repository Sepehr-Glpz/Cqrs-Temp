using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Application.CatsFeatures.Query.DTOs;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Query;
public interface ICatQueryService
{
    MetaResult<CatBasicInfo> GetCatBasicInfoById(Guid id);
    Task<MetaResult<CatBasicInfo>> GetCatBasicInfoByIdAsync(Guid id);
}

