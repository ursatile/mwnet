using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Rockaway.WebApp.Tests;

class TestFactory : WebApplicationFactory<Program> {
	public RockawayDbContext DbContext { get; } = TestDatabase.Create().Result;
	protected override void ConfigureWebHost(IWebHostBuilder builder) {
		builder.UseEnvironment("Test");
		builder.ConfigureServices(services => {
			services.RemoveAll(typeof(RockawayDbContext));
			services.AddSingleton(DbContext);
		});
	}
}