using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

public class RockawayDbContext : DbContext {
	// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
	// if we want to use Asp.NET to configure our database connection and provider.
	public RockawayDbContext(DbContextOptions<RockawayDbContext> options) : base(options) { }

	public DbSet<Artist> Artists { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		// Override EF Core's default table naming (which pluralizes entity names)
		// and use the same names as the C# classes instead
		foreach (var entity in modelBuilder.Model.GetEntityTypes()) {
			entity.SetTableName(entity.DisplayName());
		}

		modelBuilder.Entity<Artist>(entity => {
			entity.HasIndex(artist => artist.Slug).IsUnique();
		});

		modelBuilder.Entity<Artist>().HasData(SampleData.Artists.AllArtists);
	}
}