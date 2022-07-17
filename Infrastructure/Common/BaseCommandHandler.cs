using SGSX.CqrsTemp.Persistence;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Infrastructure.Common;
public abstract class BaseCommandHandler : object
{
    protected IPublisher Publisher { get; }
    protected ILogger<BaseCommandHandler> Logger { get; }
    protected BaseCommandHandler(IPublisher publisher, ILogger<BaseCommandHandler> logger) : base()
    {
        Publisher = publisher;
        Logger = logger;
    }
}

