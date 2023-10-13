namespace Rockaway.WebApp.Tests.Services;

public class TestClock : IClock {
	private readonly DateTime now;
	public TestClock(DateTime dt) {
		now = dt;
	}
	public DateTime Now => now;
}