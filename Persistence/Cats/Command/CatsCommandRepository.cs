using SGSX.CqrsTemp.Domain.Models;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SGSX.CqrsTemp.Persistence.Cats.Command;
internal class CatsCommandRepository : EFCommandRepositoryBase<Cat>, ICatsCommandRepository
{
    internal CatsCommandRepository(DbContext dbContext, ILogger<CatsCommandRepository> logger) : base(dbContext, logger)
    {
    }

    public override Result DeleteById(Guid id)
    {
        var entry = DbSet.Attach(new Cat(id, default, null, null, null!, null!, default));
        entry.State = EntityState.Deleted;
        return Result.CreateSuccessful();
    }

    public override Task<Result> DeleteByIdAsync(Guid id)
    {
        var entry = DbSet.Attach(new Cat(id, default, null, null, null!, null!, default));
        entry.State = EntityState.Deleted;
        return Task.FromResult(Result.CreateSuccessful());
    }

    public MetaResult<Cat> GetCatByName(string name)
    {
        var cat = DbSet
            .Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();
        if(cat is default(Cat))
        {
            return MetaResult<Cat>.CreateFailed().WithValidation(Validation.FromApplicationError("No record found!"));
        }
        else
        {
            return MetaResult<Cat>.CreateSuccessful().WithValue(cat);
        }
    }

    public async Task<MetaResult<Cat>> GetCatByNameAsync(string name)
    {
        var cat = await DbSet
            .Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefaultAsync();
        if (cat is default(Cat))
        {
            return MetaResult<Cat>.CreateFailed().WithValidation(Validation.FromApplicationError("No record found!"));
        }
        else
        {
            return MetaResult<Cat>.CreateSuccessful().WithValue(cat);
        }
    }

    public MetaResult<Cat> GetFullByIdAsNoTracking(Guid id)
    {
        try
        {
            var cat = DbSet
                .Where(c => c.Id == id)
                .Include(inc => inc.MouseBuddy)
                .AsNoTracking()
                .FirstOrDefault();
            if(cat is default(Cat))
            {
                return MetaResult<Cat>.CreateFailed().WithValidation(Validation.FromApplicationError("Failed to fetch record!"));
            }
            return MetaResult<Cat>.CreateSuccessful(cat);
        }
        catch(Exception ex)
        {
            return MetaResult<Cat>.CreateFailed().WithValidation(ex);
        }
    }

    public async Task<MetaResult<Cat>> GetFullByIdAsNoTrackingAsync(Guid id)
    {
        try
        {
            var cat = await DbSet
                .Where(c => c.Id == id)
                .Include(inc => inc.MouseBuddy)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (cat is default(Cat))
            {
                return MetaResult<Cat>.CreateFailed().WithValidation(Validation.FromApplicationError("Failed to fetch record!"));
            }
            return MetaResult<Cat>.CreateSuccessful(cat);
        }
        catch (Exception ex)
        {
            return MetaResult<Cat>.CreateFailed().WithValidation(ex);
        }
    }
}

