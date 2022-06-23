using MediatR;
using SGSX.CqrsTemp.Domain.Models;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Event.Events;
public class CatCreatedEvent : INotification
{
    public CatCreatedEvent()
    {
    }

    public Cat? Payload { get; init; }
}

