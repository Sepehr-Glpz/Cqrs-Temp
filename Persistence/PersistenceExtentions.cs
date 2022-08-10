using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SGSX.CqrsTemp.Persistence.Cats.Query;
using MongoDB.Driver;

namespace SGSX.CqrsTemp.Persistence;
public static class PersistenceExtentions
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration["ConnectionString"];
        services.AddDbContext<CommandDatabaseContext>(builder =>
        {
            builder.UseSqlServer(connectionString: connectionString ?? "Server=.;Database=CQRSTemp;User Id=sa;Password=1234512345", options =>
            {
                options.MigrationsHistoryTable("[EF-Migrations]");
            });
        });
        services.AddScoped<ICommandUnitOfWork, CommandUnitOfWork>();
        services.AddScoped<ICatsQueryRepository, CatsQueryRepository>();
        services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(new MongoClientSettings()
        {
            Server = new MongoServerAddress("localhost", 27017),
        }));
    }
}

