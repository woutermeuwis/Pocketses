using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pocketses.Core.Models;
using Pocketses.Core.Models.Base;

namespace Pocketses.Core.DataAccessLayer;

public class PocketsesContext : IdentityDbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    internal DbSet<User> Users { get; set; }
    internal DbSet<Campaign> Campaigns { get; set; }
    internal DbSet<Player> Players { get; set; }
    internal DbSet<Character> Characters { get; set; }


    public PocketsesContext(DbContextOptions<PocketsesContext> contextOptions, IHttpContextAccessor httpContextAccessor) : base(contextOptions)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        System.Diagnostics.Debug.Write(_httpContextAccessor.HttpContext.User.Identity.Name);

        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditedEntity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((AuditedEntity)entry.Entity).CreatedAtUtc = DateTime.UtcNow;
            }

            ((AuditedEntity)entry.Entity).UpdatedAtUtc = DateTime.UtcNow;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }


}
