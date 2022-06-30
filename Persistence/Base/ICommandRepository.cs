using SGSX.CqrsTemp.Domain.Base;
using SGSX.CqrsTemp.Domain.Results;

namespace SGSX.CqrsTemp.Persistence.Base;
public interface ICommandRepository<TEntity> : IQueryRepository<TEntity> where TEntity : BaseEntity
{
    Result Create(TEntity entity);
    Task<Result> CreateAsync(TEntity entity);

    Result Update(TEntity entity);
    Task<Result> UpdateAsync(TEntity entity);

    Result DeleteById(Guid id);
    Task<Result> DeleteByIdAsync(Guid id);
}
