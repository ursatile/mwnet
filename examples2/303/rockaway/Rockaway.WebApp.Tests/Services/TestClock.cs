namespace Rockaway.WebApp.Tests.Services;

public class TestClock : IClock {
	private readonly DateTime now;
	public TestClock(DateTime? dt = null) {
		now = dt ?? DateTime.Now;
	}
	public DateTime Now => now;
}