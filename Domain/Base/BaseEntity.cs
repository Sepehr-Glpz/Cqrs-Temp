namespace SGSX.CqrsTemp.Domain.Base;
public abstract class BaseEntity : object
{
    protected BaseEntity() => 
        (Id, CreateDate) = (Guid.NewGuid(), DateTime.Now);

    protected BaseEntity(Guid id, DateTime createDate) =>
        (Id, CreateDate) = (id, createDate);

    public Guid Id { get; }
    public DateTime CreateDate { get; }
}

