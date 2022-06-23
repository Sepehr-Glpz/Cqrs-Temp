using MediatR;
using SGSX.CqrsTemp.Domain.Results;

namespace SGSX.CqrsTemp.Application.CatsFeatures.Query;
public interface IQueryHandler<in TQuery, TReturn> : IRequestHandler<TQuery, MetaResult<TReturn>> where TQuery : IQuery<TReturn>
{
}

