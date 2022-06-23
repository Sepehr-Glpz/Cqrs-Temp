using MediatR;
namespace SGSX.CqrsTemp.Application.CatsFeatures.Event;
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : INotification
{
}

