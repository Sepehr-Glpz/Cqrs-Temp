namespace SGSX.CqrsTemp.Persistence.Base;
internal class MongoRepositoryConfig : object
{
    public MongoRepositoryConfig(IEnumerable<string> connectionStrings, string dbName) : base()
    {
        ConnectionStrings = connectionStrings.ToArray();
        DatabaseName = dbName;
    }

    public string[] ConnectionStrings { get; init; }
    public string DatabaseName { get; init; }
}

