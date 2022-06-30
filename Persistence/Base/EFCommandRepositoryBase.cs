using SGSX.CqrsTemp.Domain.Base;
using Microsoft.EntityFrameworkCore;
using SGSX.CqrsTemp.Domain.Results;
using Microsoft.Extensions.Logging;
using SGSX.CqrsTemp.Application.Resources;

namespace SGSX.CqrsTemp.Persistence.Base;
public abstract class EFCommandRepositoryBase<TEntity> : object, ICommandRepository<TEntity> where TEntity : BaseEntity
{
    protected ILogger<EFCommandRepositoryBase<TEntity>> Logger { get; }
    protected DbSet<TEntity> DbSet { get; }
    public EFCommandRepositoryBase(DbContext databaseContext, ILogger<EFCommandRepositoryBase<TEntity>> logger) : base()
    {
        DbSet = databaseContext.Set<TEntity>();
        Logger = logger;
    }

    public virtual Result Create(TEntity entity)
    {
        try
        {
            var state = DbSet.Add(entity);
            state.State = EntityState.Added;
            return Result.CreateSuccessful();
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return Result.CreateFailed().WithValidation(Validation.FromSystemError(ex.Message));
        }
    }

    public virtual async Task<Result> CreateAsync(TEntity entity)
    {
        try
        {
            var state = await DbSet.AddAsync(entity);
            state.State = EntityState.Added;
            return Result.CreateSuccessful();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return Result.CreateFailed().WithValidation(Validation.FromSystemError(ex.Message));
        }
    }

    public abstract Result DeleteById(Guid id);

    public abstract Task<Result> DeleteByIdAsync(Guid id);

    public MetaResult<IReadOnlyList<TEntity>> GetAll()
    {
        try
        {
            var entries = DbSet.ToArray();
            return MetaResult<IReadOnlyList<TEntity>>
                .CreateSuccessful()
                .WithValue(entries);
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return MetaResult<IReadOnlyList<TEntity>>
                .CreateFailed()
                .WithValidation(Validation.FromSystemError(ex.Message));
        }
    }

    public async Task<MetaResult<IReadOnlyList<TEntity>>> GetAllAsync()
    {
        try
        {
            var entries = await DbSet.ToArrayAsync();
            return MetaResult<IReadOnlyList<TEntity>>
                .CreateSuccessful()
                .WithValue(entries);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return MetaResult<IReadOnlyList<TEntity>>
                .CreateFailed()
                .WithValidation(Validation.FromSystemError(ex.Message));
        }
    }

    public MetaResult<TEntity> GetById(Guid id)
    {
        try
        {
            var entity = DbSet.Where(c => c.Id == id).FirstOrDefault();
            return MetaResult<TEntity>.CreateSuccessful().WithValue(entity);
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return MetaResult<TEntity>
                .CreateFailed()
                .WithValidation(Validation.FromSystemError(ex.Message));
        }
    }

    public async Task<MetaResult<TEntity>> GetByIdAsync(Guid id)
    {
        try
        {
            var entity = await DbSet.Where(c => c.Id == id).FirstOrDefaultAsync();
            return MetaResult<TEntity>.CreateSuccessful().WithValue(entity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return MetaResult<TEntity>
                .CreateFailed()
                .WithValidation(Validation.FromSystemError(ex.Message));
        }
    }

    public Result Update(TEntity entity)
    {
        try
        {
            var update = DbSet.Update(entity);
            return Result.CreateSuccessful();
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return Result.CreateFailed().WithValidation(Validation.FromSystemError(ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(TEntity entity)
    {
        try
        {
            var update = await Task.Run(() =>
            {
                return DbSet.Update(entity);
            });
            return Result.CreateSuccessful();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return Result.CreateFailed().WithValidation(Validation.FromSystemError(ex.Message));
        }
    }
}

