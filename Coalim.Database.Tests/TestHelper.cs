using Coalim.Database.Accessor;

namespace Coalim.Database.Tests;

public static class TestHelper
{
    public static CoalimDatabaseContext CreateDb()
    {
        CoalimTestDatabaseSchemaContext context = new();
        CoalimDatabaseContext database = new(context);

        return database;
    }
}