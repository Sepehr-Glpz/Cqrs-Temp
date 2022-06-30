
using SGSX.CqrsTemp.Domain.Base;
using SGSX.CqrsTemp.Domain.Results;

namespace SGSX.CqrsTemp.Persistence.Base;
public interface IQueryRepository<TEntity> where TEntity : BaseEntity
{
    MetaResult<TEntity> GetById(Guid id);
    Task<MetaResult<TEntity>> GetByIdAsync(Guid id);

    MetaResult<IReadOnlyList<TEntity>> GetAll();
    Task<MetaResult<IReadOnlyList<TEntity>>> GetAllAsync();
}

