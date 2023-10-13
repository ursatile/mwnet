using Microsoft.Extensions.Primitives;

namespace Rockaway.WebApp.Services;

public interface IStatusReporter {
	public ServerStatus GetStatus();
}