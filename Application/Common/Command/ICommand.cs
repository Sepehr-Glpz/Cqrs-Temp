using MediatR;
using SGSX.CqrsTemp.Domain.Results;
namespace SGSX.CqrsTemp.Application.Common.Command;
public interface ICommand : IRequest<Result>
{
}

