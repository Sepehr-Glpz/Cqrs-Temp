using Microsoft.Extensions.Logging;
using Infrastructure.Common;
using SGSX.CqrsTemp.Application.CatsFeatures.Event.Events;
using SGSX.CqrsTemp.Application.Common.Event;
using SGSX.CqrsTemp.Persistence;
using SGSX.CqrsTemp.Persistence.Cats.Query;

namespace Infrastructure.CatsFeatures.EventHandlers;
internal class CatCreatedEventHandler : BaseSyncEventHandler, IEventHandler<CatCreatedEvent>
{
    protected ICatsQueryRepository CatsQueryRepository { get; }
    public CatCreatedEventHandler(ICommandUnitOfWork unitOfWork, ILogger<CatCreatedEventHandler> logger, ICatsQueryRepository catsQueryRepository) : base(logger,unitOfWork)
    {
        CatsQueryRepository = catsQueryRepository;
    }

    public async Task Handle(CatCreatedEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            Guid id = @event.Payload;
            var acidCatResult = await CommandUnitOfWork.CatsRepository.GetFullByIdAsNoTrackingAsync(id);
            if(acidCatResult.Success != true)
            {
                Logger.LogWarning("Failed to sync Cat entity with id: {id}", id);
            }
            var createCatResult = await CatsQueryRepository.CreateCatAsync(acidCatResult.Value!);
            if(createCatResult.Success != true)
            {
                Logger.LogWarning("Failed to sync Cat into read repo! with id: {id}", id);
            }
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, ex.Message);
        }
    }
}

