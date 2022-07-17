using Microsoft.EntityFrameworkCore;
using SGSX.CqrsTemp.Domain.Models;
using SGSX.CqrsTemp.Persistence.Cats.Command;

namespace SGSX.CqrsTemp.Persistence;
internal class CommandDatabaseContext : DbContext
{
    public CommandDatabaseContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Cat>? Cats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration<Cat>(new CatModelConfiguration());
    }
}

