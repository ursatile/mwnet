using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Models;

public class ShowViewData(Show show) {

	public LocalDate ShowDate { get; set; } = show.Date;

	public string VenueName { get; set; } = show.Venue.Name;

	public string VenueAddress { get; set; } = show.Venue.FullAddress;

	public string HeadlineArtistName { get; set; } = show.HeadlineArtist.Name;

	public string CountryCode { get; set; } = show.Venue.CountryCode;

	public string CultureName { get; set; } = show.Venue.CultureName;

	public List<string> SupportActs { get; set; } = show.SupportSlots
			.OrderBy(s => s.SlotNumber)
			.Select(s => s.Artist.Name).ToList();

	public List<TicketTypeViewData> TicketTypes { get; set; }
		= show.TicketTypes.Select(tt => new TicketTypeViewData(tt)).ToList();

	public Dictionary<string, string> RouteData { get; set; } = show.RouteData;
}