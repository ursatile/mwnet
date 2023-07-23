namespace Rockaway.WebApp.Tests;

class TestDatabase {
	public static async Task<RockawayDbContext> Create(string? dbName = null) {
		dbName ??= Guid.NewGuid().ToString();
		var dbContext = Connect(dbName);
		await dbContext.Database.EnsureCreatedAsync();
		return dbContext;
	}

	public static RockawayDbContext Connect(string dbName) {
		var connectionString = $"Data Source={dbName};Mode=Memory;Cache=Shared";
		var sqliteConnection = new SqliteConnection(connectionString);
		sqliteConnection.Open();
		var options = new DbContextOptionsBuilder<RockawayDbContext>().UseSqlite(sqliteConnection).Options;
		return new(options);
	}
}