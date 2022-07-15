namespace SGSX.CqrsTemp.Persistence.Base;
internal class MongoEntityConfig
{
    public MongoEntityConfig(string tableName) : base() =>
        (EntityName) = (tableName);
    public string EntityName { get; init; }
}

