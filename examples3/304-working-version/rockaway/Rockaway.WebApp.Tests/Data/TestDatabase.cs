namespace Rockaway.WebApp.Tests.Data;

public class TestDatabase : IDisposable {
	public RockawayDbContext DbContext { get; }
	private readonly SqliteConnection sqliteConnection;

	public TestDatabase(string? dbName = null) {
		dbName ??= Guid.NewGuid().ToString();
		var connectionString = $"Data Source={dbName};Mode=Memory;Cache=Shared";
		sqliteConnection = new(connectionString);
		sqliteConnection.Open();
		var options = new DbContextOptionsBuilder<RockawayDbContext>()
			.UseSqlite(sqliteConnection)
			//.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
			.Options;
		DbContext = new(options);
		DbContext.Database.EnsureCreated();
	}

	public void Dispose() {
		sqliteConnection.Close();
	}
}