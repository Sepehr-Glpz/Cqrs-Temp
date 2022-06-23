using SGSX.CqrsTemp.Application.CatsFeatures.Query.DTOs;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Query.Queries;
public class CatBasicInfoByIdQuery : IQuery<CatBasicInfo>
{
    public CatBasicInfoByIdQuery()
    {
    }
    public Guid Id { get; init; }
}

