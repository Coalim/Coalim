using Bunkum.Core.Database;
using Coalim.Database.Schema;

namespace Coalim.Database.BunkumSupport;

public class CoalimDatabaseProvider : IDatabaseProvider<CoalimBunkumDatabaseContext>
{
    public void Initialize()
    {
        // None needed
    }

    public void Warmup()
    {
        // None needed
    }

    public CoalimBunkumDatabaseContext GetContext()
    {
        CoalimDatabaseSchemaContext context = new PostgresSchemaContext();
        return new CoalimBunkumDatabaseContext(context);
    }
    
    public void Dispose()
    {
        // TODO
    }
}