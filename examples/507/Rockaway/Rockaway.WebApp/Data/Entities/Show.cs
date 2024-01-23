using System.Globalization;

namespace Rockaway.WebApp.Data.Entities;

public class Show {
	public Venue Venue { get; set; } = default!;

	public LocalDate Date { get; set; }

	public Artist HeadlineArtist { get; set; } = default!;

	public List<SupportSlot> SupportSlots { get; set; } = [];

	private int NextSlotNumber
		=> (this.SupportSlots.Count > 0 ? this.SupportSlots.Max(s => s.SlotNumber) : 0) + 1;

	public Show WithSupportActs(params Artist[] artists) {
		foreach (var artist in artists) {
			this.SupportSlots.Add(new() {
				Show = this,
				Artist = artist,
				SlotNumber = NextSlotNumber
			});
		}
		return this;
	}

	public Dictionary<string, string> RouteData => new() {
		{ "venue", this.Venue.Slug },
		{ "date", this.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) }
	};
}