using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rockaway.WebApp.Areas.Admin.Controllers {
	[Area("admin")]
	[Authorize]
	public class HomeController : Controller {
		private readonly ILogger<HomeController> logger;

		public HomeController(ILogger<HomeController> logger) {
			this.logger = logger;
		}
		public IActionResult Index() {
			return View();
		}
	}
}