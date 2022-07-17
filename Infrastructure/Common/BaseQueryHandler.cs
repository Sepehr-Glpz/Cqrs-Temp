using Microsoft.Extensions.Logging;

namespace Infrastructure.Common;
internal abstract class BaseQueryHandler : object
{
    protected ILogger<BaseQueryHandler> Logger { get; }
    protected BaseQueryHandler(ILogger<BaseQueryHandler> logger) : base()
    {
        Logger = logger;
    }
}

