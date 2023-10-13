using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

public class RockawayDbContext : DbContext {
	// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
	// if we want to use Asp.NET to configure our database connection and provider.
	public RockawayDbContext(DbContextOptions<RockawayDbContext> options) : base(options) { }

	// A helper property which we'll use to test the connection and return the server version.
	public string ServerVersion => Database.SqlQueryRaw<string>("SELECT @@VERSION as Value").First();

	public DbSet<Artist> Artists { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder) {
		builder.Entity<Artist>(a => a.HasData(SampleData.Artists.AllArtists));
	}
}