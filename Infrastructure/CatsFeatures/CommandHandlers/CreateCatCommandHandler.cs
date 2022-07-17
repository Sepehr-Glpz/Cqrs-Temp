using Infrastructure.Common;
using SGSX.CqrsTemp.Application.CatsFeatures.Command.Commands;
using SGSX.CqrsTemp.Application.Common.Command;
using SGSX.CqrsTemp.Domain.Results;
using SGSX.CqrsTemp.Persistence;
using Microsoft.Extensions.Logging;
using SGSX.CqrsTemp.Application.CatsFeatures.Command;
using MediatR;
using SGSX.CqrsTemp.Application.CatsFeatures.Event.Events;

namespace Infrastructure.CatsFeatures.CommandHandlers;
public class CreateCatCommandHandler : BaseCommandHandler, ICommandHandler<CreateCatCommand>
{
    protected ICatCommandService CatService { get; }
    public CreateCatCommandHandler(IPublisher publisher, ILogger<CreateCatCommandHandler> logger, ICatCommandService catCommandService) : base(publisher,logger)
    {
        CatService = catCommandService;
    }
    public async Task<Result> Handle(CreateCatCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await CatService.CreatNewCatAsync(request);
            if(result.Success == true)
            {
                var catCreated = new CatCreatedEvent()
                {
                    Payload = result.Value,
                };
                await Publisher.Publish(catCreated, cancellationToken);
            }
            return result;
        }
        catch(Exception ex)
        {
            return Result.CreateFailed().WithValidation(ex);
        }
    }
}

