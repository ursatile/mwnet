namespace Rockaway.WebApp.Controllers;

public class StatusController : Controller {

	private readonly IClock clock;
	private readonly RockawayDbContext db;

	public StatusController(IClock clock, RockawayDbContext db) {
		this.clock = clock;
		this.db = db;
	}

	public IActionResult Index() {
		var model = new SystemStatus {
			DatabaseServerStatus = db.ServerVersion,
			Message = "Rockaway.WebApp is online",
			SystemTime = clock.Now
		};
		return View(model);
	}
}