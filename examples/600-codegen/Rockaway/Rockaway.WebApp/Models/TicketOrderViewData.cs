using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Models;

public class TicketOrderViewData(TicketOrder ticketOrder) {
	public string Headliner { get; } = ticketOrder.Show.HeadlineArtist.Name;
	public string VenueSummary { get; } = ticketOrder.Show.Venue.Summary;
	public LocalDate Date { get; } = ticketOrder.Show.Date;
	public bool HasSupport { get; } = ticketOrder.Show.SupportSlots.Count > 0;

	public string SupportArtistsText { get; }
		= String.Join(" + ", ticketOrder.Show.SupportArtists.Select(a => a.Name)); 

	public IEnumerable<TicketOrderItemViewData> Contents { get; }
		= ticketOrder.Contents.Select(item => new TicketOrderItemViewData(item));

	public string FormattedTotalPrice { get; } = ticketOrder.FormattedTotalPrice;
}


public class TicketOrderMailData(TicketOrder ticketOrder, Uri websiteBaseUri)
	: TicketOrderViewData(ticketOrder) {
	public string CustomerName { get; } = ticketOrder.CustomerName;
	public string CustomerEmail { get; } = ticketOrder.CustomerEmail;
	public Instant? OrderCompletedAt { get; } = ticketOrder.CompletedAt;
	public Uri BaseUri { get; } = websiteBaseUri;
	public Uri QualifyUri(string path) => new(BaseUri, path);
	public ArtistViewData Artist { get; } = new(ticketOrder.Show.HeadlineArtist);
}