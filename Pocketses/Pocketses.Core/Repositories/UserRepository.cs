using Microsoft.EntityFrameworkCore;
using Pocketses.Core.Attributes;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core.Repositories;
[ScopedDependency]
public sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(PocketsesContext ctx) : base(ctx)
    {
    }

    public async Task<User> GetOrCreateGoogleUser(string googleSubject)
    {
        var user = await Table
            .Where(u => u.GoogleSubject == googleSubject)
            .FirstOrDefaultAsync();

        if(user is null)
        {
            user = new User { GoogleSubject = googleSubject };
            await Table.AddAsync(user);
            await SaveChangesAsync();
        }

        return user;
    }
}
