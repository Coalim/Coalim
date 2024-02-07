using Bunkum.Core.Database;
using Coalim.Database.Accessor;
using Coalim.Database.Schema;
using Microsoft.EntityFrameworkCore;

namespace Coalim.Database.BunkumSupport;

public class CoalimDatabaseProvider<TSchemaType> : IDatabaseProvider<CoalimBunkumDatabaseContext>
    where TSchemaType : CoalimDatabaseSchemaContext, new()
{
    public void Initialize()
    {
        using CoalimDatabaseSchemaContext context = new TSchemaType();
        context.Database.Migrate();
    }

    public void Warmup()
    {
        // None needed
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