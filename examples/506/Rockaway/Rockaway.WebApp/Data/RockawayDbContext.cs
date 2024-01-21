using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
// if we want to use ASP.NET to configure our database connection and provider.
public class RockawayDbContext(DbContextOptions<RockawayDbContext> options)
	: IdentityDbContext<IdentityUser>(options) {

	private class InstantToDateTimeOffsetConverter()
		: ValueConverter<Instant, DateTimeOffset>(i => i.ToDateTimeOffset(), dto => Instant.FromDateTimeOffset(dto));

	private class LocalDateTimeConverter()
		: ValueConverter<LocalDateTime, DateTime>(ld => ld.ToDateTimeUnspecified(), dt => LocalDateTime.FromDateTime(dt));

	private class LocalDateConverter()
		: ValueConverter<LocalDate, DateOnly>(ld => ld.ToDateOnly(), d => LocalDate.FromDateOnly(d));

	private class LocalTimeConverter()
		: ValueConverter<LocalTime, TimeOnly>(lt => lt.ToTimeOnly(), t => LocalTime.FromTimeOnly(t));

	public DbSet<Artist> Artists { get; set; } = default!;
	public DbSet<Venue> Venues { get; set; } = default!;

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
		base.ConfigureConventions(configurationBuilder);
		configurationBuilder.Properties<Instant>().HaveConversion<InstantToDateTimeOffsetConverter>();
		configurationBuilder.Properties<LocalDate>().HaveConversion<LocalDateConverter>();
		configurationBuilder.Properties<LocalTime>().HaveConversion<LocalTimeConverter>();
		configurationBuilder.Properties<LocalDateTime>().HaveConversion<LocalDateTimeConverter>();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		// Override EF Core's default table naming (which pluralizes entity names)
		// and use the same names as the C# classes instead
		var rockawayEntityNamespace = typeof(Artist).Namespace;
		var rockawayEntities = modelBuilder.Model.GetEntityTypes().Where(e => e.ClrType.Namespace == rockawayEntityNamespace);
		foreach (var entity in rockawayEntities) entity.SetTableName(entity.DisplayName());

		modelBuilder.Entity<Artist>(entity => {
			entity.HasIndex(artist => artist.Slug).IsUnique();
		});

		modelBuilder.Entity<Venue>(entity => {
			entity.HasIndex(venue => venue.Slug).IsUnique();
		});

		modelBuilder.Entity(Show.EntityModel);
		modelBuilder.Entity(SupportSlot.EntityModel);

		modelBuilder.Entity<Artist>().HasData(SampleData.Artists.SeedData);
		modelBuilder.Entity<Venue>().HasData(SampleData.Venues.SeedData);
		modelBuilder.Entity<Show>().HasData(SampleData.Shows.SeedData);
		modelBuilder.Entity<TicketType>().HasData(SampleData.Shows.TicketTypes.SeedData);
		modelBuilder.Entity<IdentityUser>().HasData(SampleData.Users.Admin);

	}
}