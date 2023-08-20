using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Rockaway.WebApp.Tests.Data;

public static class ServiceCollectionExtensions {
	public static IServiceCollection UseTestDbContext(this IServiceCollection services, RockawayDbContext dbContext) {
		services.RemoveAll(typeof(RockawayDbContext));
		services.AddSingleton(dbContext);
		return services;
	}
}
