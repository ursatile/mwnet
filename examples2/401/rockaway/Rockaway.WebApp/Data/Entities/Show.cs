using NodaTime;
using NuGet.Packaging;

namespace Rockaway.WebApp.Data.Entities;

public class Show {
	public Show WithSupportActs(params Artist[] artists) {
		this.SupportActs.AddRange(artists.Select((artist, index) => new SupportSlot() {
			Show = this, Artist = artist, SlotNumber = index
		}));
		return this;
	}
	public Venue Venue { get; set; }
	public LocalDate Date { get; set; }
	public Artist Headliner { get; set; }
	public IList<SupportSlot> SupportActs { get; set; } = new List<SupportSlot>();
	public LocalTime DoorsOpen { get; set; }
	public LocalTime ShowBegins { get; set; }
	public LocalTime ShowEnds { get; set; }
	public LocalDateTime SalesBegin { get; set; }
	public IList<Ticket> Tickets { get; set; } = new List<Ticket>();


}
