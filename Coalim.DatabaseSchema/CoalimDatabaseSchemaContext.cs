﻿namespace Coalim.DatabaseSchema;

#nullable disable

public abstract class CoalimDatabaseSchemaContext : DbContext
{
    public DbSet<CoalimUser> Users { get; set; }

    protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
}