namespace Coalim.Database.Schema;

#nullable disable

public abstract class CoalimDatabaseSchemaContext : DbContext
{
    public DbSet<CoalimUser> Users { get; set; }
    public DbSet<CoalimServer> Servers { get; set; }
    public DbSet<CoalimChannel> Channels { get; set; }

    protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
}