namespace Rockaway.WebApp.Tests;

internal class TestFactory : WebApplicationFactory<Program> {
    
	private readonly IClock clock;

	public TestFactory(IClock clock) {
		this.clock = clock;
	}
    
	protected override void ConfigureWebHost(IWebHostBuilder builder) {
		builder.UseEnvironment("Test");
		builder.ConfigureServices(services => {
			services.AddSingleton<IClock>(clock);
		});
	}
}