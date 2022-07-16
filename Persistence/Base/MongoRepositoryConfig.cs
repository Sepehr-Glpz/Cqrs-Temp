namespace SGSX.CqrsTemp.Persistence.Base;
internal class MongoRepositoryConfig : object
{
    public MongoRepositoryConfig(string dbName, string entityName) : base()
    {
        DatabaseName = dbName;
        EntityName = entityName;
    }
    public string EntityName { get; init; }
    public string DatabaseName { get; init; }
}

