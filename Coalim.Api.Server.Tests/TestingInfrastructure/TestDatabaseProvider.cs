using Bunkum.Core.Database;
using Coalim.Database.BunkumSupport;
using Coalim.Database.Schema.Tests;

namespace Coalim.Api.Server.Tests.TestingInfrastructure;

public class TestDatabaseProvider : IDatabaseProvider<CoalimBunkumDatabaseContext>
{
    private CoalimBunkumDatabaseContext _context = null!;
    
    public void Initialize()
    {
        CoalimTestDatabaseSchemaContext context = new();
        context.Database.EnsureCreated();

        this._context = new CoalimBunkumDatabaseContext(context);
    }

    public CoalimBunkumDatabaseContext GetContext()
    {
        return this._context;
    }
    
    public void Warmup()
    {
        // None required
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}