using Coalim.Database.Accessor.Exceptions;
using Coalim.Database.Schema;
using Coalim.Database.Schema.Data;

namespace Coalim.Database.Accessor;

public class CoalimDatabaseContext : IDisposable
{
    private readonly CoalimDatabaseSchemaContext _context;
    
    public CoalimDatabaseContext(CoalimDatabaseSchemaContext context)
    {
        this._context = context;
    }

    public void SaveChanges()
    {
        this._context.SaveChanges();
    }

    public CoalimUser? GetUserByGuid(Guid guid)
    {
        return this._context.Users.FirstOrDefault(u => u.UserId == guid);
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

    public void Dispose()
    {
        if (this._context.ChangeTracker.HasChanges())
        {
            throw new UnsavedChangesException();
        }
        
        this._context.Dispose();
        GC.SuppressFinalize(this);
    }
}