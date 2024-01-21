using NodaTime;

namespace Rockaway.WebApp.Data.Entities;

public class Show {
	public Venue Venue { get; set; } = default!;

	public LocalDate Date { get; set; }

	public Artist HeadlineArtist { get; set; } = default!;

	public List<SupportSlot> SupportSlots { get; set; } = [];

	public List<TicketType> TicketTypes { get; set; } = [];

	public Show WithTicketType(string name, decimal price) {
		this.TicketTypes.Add(new(this, name, price));
		return this;
	}

	public static Action<EntityTypeBuilder<Show>> EntityModel => show => {
		show.HasMany(s => s.SupportSlots).WithOne(ss => ss.Show).OnDelete(DeleteBehavior.Cascade);
		show.HasMany(s => s.TicketTypes).WithOne(tt => tt.Show).OnDelete(DeleteBehavior.Cascade);
		show.HasKey(nameof(Venue) + nameof(Venue.Id), nameof(Date));
	};
}