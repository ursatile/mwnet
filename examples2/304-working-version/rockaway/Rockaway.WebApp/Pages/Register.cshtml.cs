using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rockaway.WebApp.Pages;

public class RegisterModel : PageModel {
	public string[] AllInterests = new[] {
		"Pop", "Rock", "Jazz", "Classical", "Drill", "Trap", "Djent"
	};

	public bool Registered { get; set; }
	public string Email { get; set; } = String.Empty;
	public string[] Interests { get; set; } = Array.Empty<string>();
	public void OnGet() {
		Registered = false;
	}

	public void OnPost(string email, string[] interests) {

		//TODO: actually sign up the user to a mailing list

		Email = email;
		Interests = interests;
		Registered = true;
	}
}