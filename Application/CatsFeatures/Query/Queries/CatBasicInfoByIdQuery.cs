using SGSX.CqrsTemp.Application.CatsFeatures.Query.DTOs;
using SGSX.CqrsTemp.Application.Common.Query;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Query.Queries;
public class CatBasicInfoByIdQuery : IQuery<CatBasicInfo>
{
    public CatBasicInfoByIdQuery()
    {
    }
    public Guid Id { get; init; }
}

