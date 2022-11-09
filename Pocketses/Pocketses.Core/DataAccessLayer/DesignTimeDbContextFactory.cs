using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pocketses.Core.DataAccessLayer;

internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PocketsesContext>
{
    public PocketsesContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PocketsesContext>();
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Join(path, "Pocketses.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new PocketsesContext(optionsBuilder.Options, null);
    }
}
