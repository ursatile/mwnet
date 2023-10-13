using Microsoft.AspNetCore.Mvc;
using Rockaway.WebApp.Models;
using Rockaway.WebApp.Services;

namespace Rockaway.WebApp.Controllers;

public class StatusController : Controller {

    private readonly IClock clock;
    public StatusController(IClock clock) {
        this.clock = clock;
    }

    public IActionResult Index() {
        var model = new SystemStatus {
            Message = "Rockaway.WebApp is online",
            SystemTime = clock.Now
        };
        return View(model);
    }
}
