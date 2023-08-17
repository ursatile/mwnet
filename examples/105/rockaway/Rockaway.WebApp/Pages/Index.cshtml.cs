using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rockaway.WebApp.Pages;

public class IndexModel : PageModel {

	public string Heading { get; set; } = "Hello World!";
	public void OnGet() {
	}
}