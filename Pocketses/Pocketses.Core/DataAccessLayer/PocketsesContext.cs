using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pocketses.Core.Extensions;
using Pocketses.Core.Models;
using Pocketses.Core.Models.Base;

namespace Pocketses.Core.DataAccessLayer;

public class PocketsesContext : IdentityDbContext
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	internal DbSet<User> Users { get; set; }
	internal DbSet<Campaign> Campaigns { get; set; }
	internal DbSet<Character> Characters { get; set; }


	public PocketsesContext(DbContextOptions<PocketsesContext> contextOptions, IHttpContextAccessor httpContextAccessor) : base(contextOptions)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var userId = _httpContextAccessor?.GetUserId();
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

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		ConfigureCampaigns(builder);
		ConfigureUsers(builder);
		ConfigureCharacters(builder);
	}

	private void ConfigureCampaigns(ModelBuilder builder)
	{

		builder.Entity<Campaign>()
			.HasOne(c => c.DungeonMaster)
			.WithMany(u => u.DmCampaigns)
			.IsRequired()
			.HasForeignKey(c => c.DungeonMasterId);

		builder.Entity<Campaign>()
			.HasMany(c => c.Players)
			.WithMany(u => u.UserCampaigns);

		builder.Entity<Campaign>()
			.HasMany(c => c.Characters)
			.WithOne()
			.HasForeignKey(c => c.CampaignId);

	}

	private void ConfigureUsers(ModelBuilder builder)
	{
		builder.Entity<User>()
			.HasMany(u => u.DmCampaigns)
			.WithOne(c => c.DungeonMaster)
			.IsRequired()
			.HasForeignKey(c => c.DungeonMasterId);

		builder.Entity<User>()
			.HasMany(u => u.UserCampaigns)
			.WithMany(c => c.Players);

		builder.Entity<User>()
			.HasMany(u => u.Characters)
			.WithOne(c => c.User)
			.HasForeignKey(c => c.UserId);
	}

	private void ConfigureCharacters(ModelBuilder builder)
	{
		builder.Entity<Character>()
			.HasOne(c => c.User)
			.WithMany(u => u.Characters)
			.HasForeignKey(c => c.UserId);

		builder.Entity<Character>()
			.HasOne(c => c.Campaign)
			.WithMany(c => c.Characters)
			.HasForeignKey(c => c.CampaignId);

		builder.Entity<Character>()
			.Navigation(c => c.User)
			.AutoInclude();

		builder.Entity<Character>()
			.Navigation(c => c.Campaign)
			.AutoInclude();

	}

}
