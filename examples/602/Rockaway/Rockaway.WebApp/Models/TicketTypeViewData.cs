using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Models;

public class TicketTypeViewData {

	// Razor Components use System.Text.Json serialization
	// and so require a public parameterless constructor
	public TicketTypeViewData() { }

	public TicketTypeViewData(TicketType tt) {
		this.Id = tt.Id;
		this.Name = tt.Name;
		this.Price = tt.Price;
	}

	public Guid Id { get; set; }
	public string Name { get; set; } = String.Empty;
	public decimal Price { get; set; }
}
