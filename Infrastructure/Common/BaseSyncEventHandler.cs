using Microsoft.Extensions.Logging;
using SGSX.CqrsTemp.Persistence;

namespace Infrastructure.Common;
internal abstract class BaseSyncEventHandler : object
{
    protected ICommandUnitOfWork CommandUnitOfWork { get; }
    protected ILogger<BaseSyncEventHandler> Logger { get; }
    public BaseSyncEventHandler(ILogger<BaseSyncEventHandler> logger, ICommandUnitOfWork unitOfWork) : base()
    {
        CommandUnitOfWork = unitOfWork;
        Logger = logger;
    }
}

