using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Rockaway.WebApp.Tests;

class TestFactory : WebApplicationFactory<Program> {

	private readonly IClock clock;
	private readonly TestDatabase tdb;

	public TestFactory(IClock? clock = null) {
		tdb = new();
		this.clock = clock ?? new TestClock();
	}

	public RockawayDbContext DbContext => tdb.DbContext;

	protected override void ConfigureWebHost(IWebHostBuilder builder) {
		builder.UseEnvironment("Test");
		builder.ConfigureServices(services => {
			services.AddSingleton(clock);
			services.AddSingleton(tdb.DbContext);
		});
	}

	protected override void Dispose(bool disposing) {
		base.Dispose(disposing);
		tdb.Dispose();
	}
}