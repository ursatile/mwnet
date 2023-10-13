using Microsoft.AspNetCore.Mvc;
using Rockaway.WebApp.Models;

namespace Rockaway.WebApp.Controllers;

public class StatusController : Controller {
    public IActionResult Index() {
        var model = new SystemStatus {
            Message = "Rockaway.WebApp is online",
            SystemTime = DateTime.Now
        };
        return View(model);
    }
}
