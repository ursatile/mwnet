using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Models;

public class ShowViewData(Show show) {
	public string ShowDate => show.Date.ToString();
	public string VenueName => show.Venue.Name;

	public string VenueAddress => String.Join(", ", [
		show.Venue.Address, show.Venue.City, show.Venue.PostalCode
	]);

	public string CountryCode => show.Venue.CountryCode;
	public string VenueSlug => show.Venue.Slug;
}
