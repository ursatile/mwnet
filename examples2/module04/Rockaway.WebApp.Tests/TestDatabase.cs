using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Rockaway.WebApp.Tests;

class TestDatabase {

	public static readonly ILoggerFactory ConsoleLoggerFactory
		= LoggerFactory.Create(builder => builder.AddConsole());

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
		var options = new DbContextOptionsBuilder<RockawayDbContext>()
			.UseSqlite(sqliteConnection)
			.UseLoggerFactory(ConsoleLoggerFactory)
			.Options;
		return new(options);
	}
}