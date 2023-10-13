using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Rockaway.WebApp.Services;

namespace Rockaway.WebApp.Tests;

class TestFactory : WebApplicationFactory<Program> {
    
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