
using SGSX.CqrsTemp.Domain.Models;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Persistence.Base;

namespace SGSX.CqrsTemp.Persistence.Cats.Command;
public interface ICatsCommandRepository : ICommandRepository<Cat>
{
    MetaResult<Cat> GetCatByName(string name);
    Task<MetaResult<Cat>> GetCatByNameAsync(string name);
}

