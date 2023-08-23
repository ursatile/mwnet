using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Rockaway.WebApp.Tests.WebTests;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Rockaway.WebApp.Tests;

class TestFactory : WebApplicationFactory<Program> {


	private readonly IClock clock;
	private readonly TestDatabase tdb;

	public TestFactory(IClock? clock = null) {
		tdb = new();
		this.clock = clock ?? new TestClock();
	}

	private class FakeAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
		public FakeAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
			: base(options, logger, encoder, clock) { }

		protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
			var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
			var identity = new ClaimsIdentity(claims, "Test");
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, "Test");
			var result = AuthenticateResult.Success(ticket);
			return Task.FromResult(result);
		}
	}

	public WebApplicationFactory<Program> WithFakeAuth() => WithWebHostBuilder(builder => {
		builder.ConfigureTestServices(services => {
			services.AddAuthentication("FakeAuth")
				.AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>("FakeAuth", _ => { });
		});
	});

	public RockawayDbContext DbContext => tdb.DbContext;

	protected override void ConfigureWebHost(IWebHostBuilder builder) {
		builder.UseEnvironment("Test");
		builder.ConfigureServices(services => {
			services.AddAntiforgery(setup => {
				setup.Cookie.Name = AntiForgeryTokenExtractor.ANTI_FORGERY_COOKIE_NAME;
				setup.FormFieldName = AntiForgeryTokenExtractor.ANTI_FORGERY_FIELD_NAME;
			});
			services.AddSingleton(clock);
			services.AddSingleton(tdb.DbContext);

		});
	}

	protected override void Dispose(bool disposing) {
		base.Dispose(disposing);
		tdb.Dispose();
	}
}


