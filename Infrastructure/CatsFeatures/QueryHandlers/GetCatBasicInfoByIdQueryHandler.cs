using Microsoft.Extensions.Logging;
using Infrastructure.Common;
using SGSX.CqrsTemp.Application.CatsFeatures.Query.DTOs;
using SGSX.CqrsTemp.Application.CatsFeatures.Query.Queries;
using SGSX.CqrsTemp.Application.Common.Query;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Application.CatsFeatures.Query;

namespace Infrastructure.CatsFeatures.QueryHandlers;
internal class GetCatBasicInfoByIdQueryHandler : BaseQueryHandler, IQueryHandler<CatBasicInfoByIdQuery, CatBasicInfo>
{
    protected ICatQueryService CatService { get; }
    public GetCatBasicInfoByIdQueryHandler(ILogger<GetCatBasicInfoByIdQueryHandler> logger, ICatQueryService catQueryService) : base(logger)
    {
        CatService = catQueryService;
    }
    public async Task<MetaResult<CatBasicInfo>> Handle(CatBasicInfoByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var catBasicInfoResult = await CatService.GetCatBasicInfoByIdAsync(request.Id);
            return catBasicInfoResult;
        }
        catch(Exception ex)
        {
            return MetaResult<CatBasicInfo>.CreateFailed().WithValidation(ex);
        }
    }
}

