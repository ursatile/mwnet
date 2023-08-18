namespace Rockaway.WebApp.Tests;

class TestFactory : WebApplicationFactory<Program> {

	private readonly IClock clock;

	public TestFactory(IClock clock) {
		this.clock = clock;
	}

	public RockawayDbContext DbContext { get; } = TestDatabase.CreateAsync().Result;

	protected override void ConfigureWebHost(IWebHostBuilder builder) {
		builder.UseEnvironment("Test");
		builder.ConfigureServices(services => {
			services.AddSingleton(clock);
			services.AddSingleton(DbContext);
		});
	}
}