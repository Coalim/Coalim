using Coalim.Database.Schema;
using Coalim.Database.Schema.Data;

namespace Coalim.Database.Accessor;

public class CoalimDatabaseContext : IDisposable, IAsyncDisposable
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
        this.SaveChanges();
        return user;
    }

    public void Dispose()
    {
        this._context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}