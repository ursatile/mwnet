using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using NodaTime;

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
}

public class Show {
	public Venue Venue { get; set; }
	public LocalDate Date { get; set; }
	public IList<Artist> Artists { get; set; } = new List<Artist>();
	public LocalTime DoorsOpen { get; set; }
	public LocalTime ShowBegins { get; set; }
	public LocalTime ShowEnds { get; set; }
	public LocalDateTime SalesBegin { get; set; }
	public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
}

public class Ticket {
	public Guid Id { get; set; }
	public Show Show { get; set; }
	public decimal Price { get; set; }
	public int SalesLimit { get; set; }
}