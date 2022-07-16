using SGSX.CqrsTemp.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace SGSX.CqrsTemp.Persistence.Base;
internal class UnitOfWork : IUnitOfWork
{
    protected DbContext? DatabaseContext { get; private set; }
    internal UnitOfWork(DbContext dbContext) : base() =>
        (DatabaseContext, IsDisposed) = (dbContext, false);

    public bool IsDisposed { get; private set; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    private void Dispose(bool disposing)
    {
        if(IsDisposed)
        {
            return;
        }    
        if(disposing)
        {
            DatabaseContext?.Dispose();
        }

        DatabaseContext = null;

        IsDisposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }
        if (disposing)
        {
           await DatabaseContext!.DisposeAsync();
        }

        DatabaseContext = null;

        IsDisposed = true;
    }

    public Result SaveChanges()
    {
        try
        {
            DatabaseContext!.SaveChanges();
            return Result.BasicSuccess;
        }
        catch(DbUpdateException ex)
        {
            return Result.CreateFailed().WithValidation(ex);
        }
    }

    public async Task<Result> SaveChangesAsync()
    {
        try
        {
            await DatabaseContext!.SaveChangesAsync();
            return Result.BasicSuccess;
        }
        catch (DbUpdateException ex)
        {
            return Result.CreateFailed().WithValidation(ex);
        }
    }
}

