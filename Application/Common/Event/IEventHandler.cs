using MediatR;
namespace SGSX.CqrsTemp.Application.Common.Event;
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : INotification
{
}

