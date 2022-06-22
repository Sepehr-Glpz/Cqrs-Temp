using SGSX.CqrsTemp.Domain.Base;

namespace SGSX.CqrsTemp.Domain.Models;
public class MouseBuddy : BaseEntity
{
    public MouseBuddy(Guid catId, Cat owner, string name, uint attackPower) : base() =>
        (CatId, Owner, Name, AttackPower) = (catId, owner, name, attackPower);
    public MouseBuddy(Guid id, DateTime createDate, Guid catId, Cat owner, string name, uint attackPower) : base(id, createDate) =>
        (CatId, Owner, Name, AttackPower) = (catId, owner, name, attackPower);

    public Guid CatId { get; }

    public virtual Cat Owner { get; }

    public string Name { get; internal protected set; }

    public uint AttackPower { get; internal protected set; } 
}

