namespace Rockaway.WebApp.Areas.Admin.Controllers;

public class CheckoutController : Controller {
	public IActionResult Details(Guid id) {
		return Ok(id);
	}
}