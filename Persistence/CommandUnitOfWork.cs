using SGSX.CqrsTemp.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using SGSX.CqrsTemp.Persistence.Cats.Query;
using SGSX.CqrsTemp.Persistence.Cats.Command;
using Microsoft.Extensions.Logging;

namespace SGSX.CqrsTemp.Persistence;
internal class CommandUnitOfWork : UnitOfWork, ICommandUnitOfWork
{
    protected ILoggerFactory LoggerProvider { get; }
    internal CommandUnitOfWork(CommandDatabaseContext dbContext, ILoggerFactory loggerProvider) : base(dbContext)
    {
        LoggerProvider = loggerProvider;
    }

    private ICatsCommandRepository? _catsCommandRepository;
    public ICatsCommandRepository CatsRepository 
        { get => _catsCommandRepository ??= new CatsCommandRepository(DatabaseContext!, LoggerProvider.CreateLogger<CatsCommandRepository>()); }
}

