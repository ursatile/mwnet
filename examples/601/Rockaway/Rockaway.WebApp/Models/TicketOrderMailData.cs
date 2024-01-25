using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Models;

public class TicketOrderMailData(TicketOrder ticketOrder, Uri websiteBaseUri)
	: TicketOrderViewData(ticketOrder) {
	public string CustomerName { get; } = ticketOrder.CustomerName;
	public string CustomerEmail { get; } = ticketOrder.CustomerEmail;
	public Instant? OrderCompletedAt { get; } = ticketOrder.CompletedAt;
	public Uri BaseUri { get; } = websiteBaseUri;
	public Uri QualifyUri(string path) => new(BaseUri, path);
	public ArtistViewData Artist { get; } = new(ticketOrder.Show.HeadlineArtist);
}
