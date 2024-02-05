using Coalim.Database.Schema;
using Coalim.Database.Schema.Data;

namespace Coalim.Database.Accessor;

public class CoalimDatabaseContext
{
    private readonly CoalimDatabaseSchemaContext _context;
    
    public CoalimDatabaseContext(CoalimDatabaseSchemaContext context)
    {
        this._context = context;
    }

    private void SaveChanges()
    {
        this._context.SaveChanges();
    }

    public CoalimUser CreateUser(string username)
    {
        CoalimUser user = new CoalimUser
        {
            Username = username,
        };

        this._context.Users.Add(user);
        return user;
    }
}