using SGSX.CqrsTemp.Persistence.Base;
using SGSX.CqrsTemp.Persistence.Cats.Command;

namespace SGSX.CqrsTemp.Persistence;
public interface ICommandUnitOfWork : IUnitOfWork
{
    ICatsCommandRepository CatsRepository { get; }
}

