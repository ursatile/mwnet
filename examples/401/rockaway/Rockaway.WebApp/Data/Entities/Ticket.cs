using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Rockaway.WebApp.Data.Entities;

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
