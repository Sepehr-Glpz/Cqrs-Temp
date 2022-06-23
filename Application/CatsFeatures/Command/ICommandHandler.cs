using MediatR;
using SGSX.CqrsTemp.Domain.Results;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Command;
public interface ICommandHandler<in TCommnad> : IRequestHandler<TCommnad, Result> where TCommnad : ICommand
{
}

