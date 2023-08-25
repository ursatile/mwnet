using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using NodaTime;
using NuGet.Packaging;

namespace Rockaway.WebApp.Data.Entities;

public class Artist {
	public Guid Id { get; set; }

	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;

	[MaxLength(500)]
	public string Description { get; set; } = String.Empty;

	[MaxLength(100)]
	[Unicode(false)]
	[RegularExpression(@"^[a-z0-9][a-z0-9-]{0,98}[a-z0-9]$", ErrorMessage = "Slug must be 2-100 characters and can only contain letters a-z, digits 0-9, and hyphens. It cannot start or end with a hyphen.")]
	public string Slug { get; set; } = String.Empty;

	public IList<Show> Shows { get; set; } = new List<Show>();
}

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

public class SupportSlot {
	public Show Show { get; set; } = null!;
	public int SlotNumber { get; set; }
	public Artist Artist { get; set; } = null!;
	public string Comments { get; set; } = String.Empty;

	//	[NotMapped]
	//	public Guid? ShowVenueId => Show?.Venue?.Id;
	//	[NotMapped]
	//	public LocalDate? ShowDate => Show?.Date;
}

	public class Ticket {
	public Guid Id { get; set; }
	public Show Show { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
	public int SalesLimit { get; set; }

	[NotMapped]
	public Guid? ShowVenueId => Show?.Venue?.Id;
	[NotMapped]
	public LocalDate? ShowDate => Show?.Date;
}