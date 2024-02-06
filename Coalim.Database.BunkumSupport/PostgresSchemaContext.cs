using Coalim.Database.Schema;
using Microsoft.EntityFrameworkCore;

namespace Coalim.Database.BunkumSupport;

public class PostgresSchemaContext : CoalimDatabaseSchemaContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=coalim;Username=coalim;Password=coalim");
    }
}