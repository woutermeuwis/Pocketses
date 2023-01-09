using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pocketses.Core.DataAccessLayer;

namespace Pocketses.Core.Models;

public static class SeedData
{
	public static void Initialize(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		using var ctx = new PocketsesContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<PocketsesContext>>(), null);

		if (ctx.Campaigns.Any())
			return; // DB has been seeded already



		ctx.SaveChanges();
	}
}
