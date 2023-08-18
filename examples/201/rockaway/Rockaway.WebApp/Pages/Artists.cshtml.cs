using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rockaway.WebApp.Pages;

public class ArtistsModel : PageModel {

	public string Name { get; set; } = "";
	public void OnGet(string name) {
		this.Name = name;
	}
}