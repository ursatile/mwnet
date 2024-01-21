namespace Rockaway.WebApp.Data.Entities;

public class TicketType(Show show, string name, decimal price) {

	public Guid Id { get; set; } = Guid.NewGuid();
	public Show Show { get; set; } = show;
	public string Name { get; set; } = name;
	public decimal Price { get; set; } = price;

	// Private constructor required by EF Core
	private TicketType() : this(default!, default!, default) {
		Id = Guid.Empty;
	}
}
