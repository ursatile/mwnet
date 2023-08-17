namespace Rockaway.WebApp.Services;

public class SystemClock : IClock {
	public DateTime Now => DateTime.Now;
}