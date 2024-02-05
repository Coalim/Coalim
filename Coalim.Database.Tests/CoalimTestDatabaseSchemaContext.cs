using Coalim.Database.Schema;
using Microsoft.EntityFrameworkCore;

namespace Coalim.Database.Tests;

public class CoalimTestDatabaseSchemaContext : CoalimDatabaseSchemaContext
{
    private static uint _idIncrement = 0;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(_idIncrement++.ToString());
    }
}