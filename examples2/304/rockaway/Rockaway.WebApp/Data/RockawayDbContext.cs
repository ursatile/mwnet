using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

public class RockawayDbContext : IdentityDbContext<IdentityUser> {
	// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
	// if we want to use Asp.NET to configure our database connection and provider.
	public RockawayDbContext(DbContextOptions<RockawayDbContext> options) : base(options) { }

	public DbSet<Artist> Artists { get; set; } = null!;
	public DbSet<Venue> Venues { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);
		builder.Entity<Artist>(artist => {
			artist.HasData(SampleData.Artists.AllArtists);
			artist.HasIndex(a => a.Slug).IsUnique();
		});
		builder.Entity<Venue>(venue => venue.HasData(SampleData.Venues.AllVenues));
		builder.Entity<IdentityUser>(users => users.HasData(SampleData.Users.Admin));
	}

	private string DbVersionExpression {
		get {
			if (Database.IsSqlServer()) return "@@VERSION";
			if (Database.IsSqlite()) return ("'SQLite ' || sqlite_version()");
			throw new Exception("Unsupported database provider");
		}
	}

	// A helper property which we'll use to test the connection and return the server version.
	public string ServerVersion => Database.SqlQueryRaw<string>($"SELECT {DbVersionExpression} as Value").Single();

}