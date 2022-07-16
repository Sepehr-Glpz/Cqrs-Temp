using SGSX.CqrsTemp.Domain.Results;

namespace SGSX.CqrsTemp.Persistence.Base;
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    public bool IsDisposed { get; }
    Result SaveChanges();
    Task<Result> SaveChangesAsync();
}

