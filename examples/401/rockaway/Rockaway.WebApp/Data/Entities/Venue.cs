using NodaTime;

namespace Rockaway.WebApp.Data.Entities;

public class Venue {
	public Venue() { }

	public Venue(Guid id, string name, string address, string city, string countryCode, string? postalCode = null,
		string? telephone = null, string? websiteUrl = null) {
		Id = id;
		Name = name;
		Address = address;
		City = city;
		CountryCode = countryCode;
		PostalCode = postalCode;
		Telephone = telephone;
		WebsiteUrl = websiteUrl;
	}

	public IList<Show> Shows { get; set; } = new List<Show>();

	public Show BookShow(Artist headliner, LocalDate date) {
		var show = new Show() {
			Headliner = headliner,
			Date = date,
			DoorsOpen = new(18, 00),
			ShowBegins = new(19, 00),
			Venue = this,
			ShowEnds = new(23, 00),
		};
		this.Shows.Add(show);
		return show;
	}

	public Guid Id { get; set; }

	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;

	[MaxLength(500)]
	public string Address { get; set; } = String.Empty;

	[MaxLength(100)]
	public string City { get; set; } = String.Empty;

	[MaxLength(2)]
	[Unicode(false)]
	public string CountryCode { get; set; } = String.Empty;

	[MaxLength(10)]
	[Unicode(false)]
	public string? PostalCode { get; set; }

	[MaxLength(255)]
	public string? WebsiteUrl { get; set; }

	[MaxLength(32)]
	[Unicode(false)]
	public string? Telephone { get; set; }

}