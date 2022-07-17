using SGSX.CqrsTemp.Persistence;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Common;
internal abstract class BaseCommandService : object, IDisposable, IAsyncDisposable
{
    protected ICommandUnitOfWork? UnitOfWork { get; set; }
    protected ILogger<BaseCommandService> Logger { get; }
    protected BaseCommandService(ICommandUnitOfWork unitOfWork, ILogger<BaseCommandService> logger) =>
        (UnitOfWork, IsDisposed, Logger) = (unitOfWork, false, logger);

    ~BaseCommandService()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return DisposeAsync(true);
    }

    public bool IsDisposed { get; protected set; }

    protected virtual void Dispose(bool disposing)
    {
        if(IsDisposed)
        {
            return;
        }
        if(disposing)
        {
            if(UnitOfWork?.IsDisposed != true)
            {
                UnitOfWork!.Dispose();
                UnitOfWork = null;
            }
        }
        UnitOfWork = null;

        IsDisposed = false;
    }
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }
        if (disposing)
        {
            if (UnitOfWork?.IsDisposed != true)
            {
                await UnitOfWork!.DisposeAsync();
                UnitOfWork = null;
            }
        }
        UnitOfWork = null;

        IsDisposed = false;
    }

}

