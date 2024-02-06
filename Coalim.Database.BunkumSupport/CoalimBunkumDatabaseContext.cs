using Bunkum.Core.Database;
using Coalim.Database.Accessor;
using Coalim.Database.Schema;

namespace Coalim.Database.BunkumSupport;

public class CoalimBunkumDatabaseContext : CoalimDatabaseContext, IDatabaseContext
{
    public CoalimBunkumDatabaseContext(CoalimDatabaseSchemaContext context) : base(context)
    {}
}