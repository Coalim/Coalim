using Bunkum.Core.Database;
using Coalim.Database.Schema;

namespace Coalim.Database.BunkumSupport;

public class CoalimDatabaseProvider<TSchemaType> : IDatabaseProvider<CoalimBunkumDatabaseContext>
    where TSchemaType : CoalimDatabaseSchemaContext, new()
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
        CoalimDatabaseSchemaContext context = new TSchemaType();
        context.Database.EnsureCreated(); // TODO: don't do this
        return new CoalimBunkumDatabaseContext(context);
    }
    
    public void Dispose()
    {
        // TODO
    }
}