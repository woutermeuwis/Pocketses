using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pocketses.Core.Models;

namespace Pocketses.Core.DataAccessLayer;

public class PocketsesContext : IdentityDbContext
{
    public PocketsesContext(DbContextOptions<PocketsesContext> contextOptions) : base(contextOptions)
    {
    }

    internal DbSet<Campaign> Campaigns { get; set; }
    internal DbSet<Player> Players { get; set; }
    internal DbSet<Character> Characters { get; set; }
}
