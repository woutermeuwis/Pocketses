using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Data.Entity.Infrastructure;

namespace Pocketses.Core.DataAccessLayer;

internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PocketsesContext>
{
    public PocketsesContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PocketsesContext>();
        optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=PocketsesDb;Trusted_Connection=True;");
        return new PocketsesContext(optionsBuilder.Options);
    }
}
