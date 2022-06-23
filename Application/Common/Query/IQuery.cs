using MediatR;
using SGSX.CqrsTemp.Domain.Results;

namespace SGSX.CqrsTemp.Application.Common.Query;
public interface IQuery<TReturn> : IRequest<MetaResult<TReturn>>
{
}

