namespace Rockaway.WebApp.Data.Sample;

public static class SeedDataConverters {

	public static object ToSeedData(this Show show) => new {
		VenueId = show.Venue.Id,
		HeadlinerId = show.Headliner.Id,
		show.Date,
		show.DoorsOpen,
		show.ShowBegins,
		show.SalesBegin,
		show.ShowEnds
	};

	public static object ToSeedData(this Venue venue) => new {
		venue.Id,
		venue.Name,
		venue.Address,
		venue.City,
		venue.CountryCode,
		venue.PostalCode,
		venue.Telephone,
		venue.WebsiteUrl
	};

	public static object ToSeedData(this SupportSlot slot) => new {
		ShowVenueId = slot.Show.Venue.Id,
		ShowDate = slot.Show.Date,
		ArtistId = slot.Artist.Id,
		slot.SlotNumber,
		slot.Comments
	};
}
