namespace Rockaway.WebApp.Tests.Data; 

class TestDatabase {

	public static async Task<RockawayDbContext> CreateAsync(string? dbName = null) {
		dbName ??= Guid.NewGuid().ToString();
		var dbContext = Connect(dbName);
		await dbContext.Database.EnsureCreatedAsync();
		return dbContext;
	}

	public static RockawayDbContext Connect(string dbName) {
		var connectionString = $"Data Source={dbName};Mode=Memory;Cache=Shared";
		var sqliteConnection = new SqliteConnection(connectionString);
		var options = new DbContextOptionsBuilder<RockawayDbContext>()
			.UseSqlite(sqliteConnection)
			.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
			.Options;
		return new(options);
	}
}