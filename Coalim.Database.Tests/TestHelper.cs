using Coalim.Database.Accessor;
using Coalim.Database.Schema.Tests;
using Microsoft.EntityFrameworkCore;

namespace Coalim.Database.Tests;

public static class TestHelper
{
    public static CoalimDatabaseContext CreateDb()
    {
        CoalimTestDatabaseSchemaContext context = new();
        context.Database.EnsureCreated();
        CoalimDatabaseContext database = new(context);

        return database;
    }
}