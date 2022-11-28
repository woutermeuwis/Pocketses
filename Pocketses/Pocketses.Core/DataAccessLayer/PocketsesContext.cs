using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
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
        var userId = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditedEntity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((AuditedEntity)entry.Entity).CreatedAtUtc = DateTime.UtcNow;
                ((AuditedEntity)entry.Entity).CreatedBy = userId;
            }

            ((AuditedEntity)entry.Entity).UpdatedAtUtc = DateTime.UtcNow;
            ((AuditedEntity)entry.Entity).UpdatedBy = userId;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }


}
