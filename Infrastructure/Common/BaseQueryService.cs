using Microsoft.Extensions.Logging;

namespace Infrastructure.Common;
internal abstract class BaseQueryService : object
{
    protected ILogger<BaseQueryService> Logger { get; }
    protected BaseQueryService(ILogger<BaseQueryService> logger) : base()
    {
        Logger = logger;
    }
}

