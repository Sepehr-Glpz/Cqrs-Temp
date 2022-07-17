using Microsoft.Extensions.Logging;
using Infrastructure.Common;
using SGSX.CqrsTemp.Application.CatsFeatures.Query;
using SGSX.CqrsTemp.Application.CatsFeatures.Query.DTOs;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Persistence.Cats.Query;

namespace Infrastructure.CatsFeatures;
internal class CatQueryService : BaseQueryService, ICatQueryService
{
    protected ICatsQueryRepository CatsRepository { get; }
    public CatQueryService(ILogger<CatQueryService> logger, ICatsQueryRepository catsQueryRepository) : base(logger)
    {
        CatsRepository = catsQueryRepository;
    }

    public MetaResult<CatBasicInfo> GetCatBasicInfoById(Guid id)
    {
        var catInfoResult = CatsRepository.GetBasicInfoById(id);
        return catInfoResult;
    }

    public async Task<MetaResult<CatBasicInfo>> GetCatBasicInfoByIdAsync(Guid id)
    {
        var catInfoResult = await CatsRepository.GetBasicInfoByIdAsync(id);
        return catInfoResult;
    }
}

