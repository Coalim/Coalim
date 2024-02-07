using Bunkum.Core.Database;
using Coalim.Database.Schema;
using Microsoft.EntityFrameworkCore;

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
        using CoalimDatabaseSchemaContext context = new TSchemaType();
        context.Database.Migrate();
    }

    public CoalimBunkumDatabaseContext GetContext()
    {
        CoalimDatabaseSchemaContext context = new TSchemaType();
        return new CoalimBunkumDatabaseContext(context);
    }
    
    public void Dispose()
    {
        // TODO
    }
}