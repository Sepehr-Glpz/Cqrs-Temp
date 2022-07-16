using SGSX.CqrsTemp.Domain.Models;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Persistence.Base;

namespace SGSX.CqrsTemp.Persistence.Cats.Query;
public interface ICatsQueryRepository : IQueryRepository<Cat>
{
    MetaResult<Cat> GetByName(string name);
    Task<MetaResult<Cat>> GetByNameAsync(string name);
}

