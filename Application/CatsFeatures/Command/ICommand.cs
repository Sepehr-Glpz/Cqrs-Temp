using MediatR;
using SGSX.CqrsTemp.Domain.Results;
namespace SGSX.CqrsTemp.Application.CatsFeatures.Command;
public interface ICommand : IRequest<Result>
{
}

