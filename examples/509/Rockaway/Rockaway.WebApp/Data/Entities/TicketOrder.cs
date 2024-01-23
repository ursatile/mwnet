namespace Rockaway.WebApp.Data.Entities;

public class TicketOrder {
	public Guid Id { get; set; }
	public List<TicketOrderItem> Contents { get; set; } = [];
	public string CustomerName { get; set; } = String.Empty;
	public string CustomerEmail { get; set; } = String.Empty;
	public Instant CommencedAt { get; set; }
	public Instant? CompletedAt { get; set; }

	public TicketOrderItem UpdateQuantity(TicketType ticketType, int quantity) {
		var item = this.Contents.FirstOrDefault(toi => toi.TicketType == ticketType);
		if (item == default) {
			item = new() { TicketOrder = this, TicketType = ticketType };
			this.Contents.Add(item);
		}
		item.Quantity = quantity;
		return item;
	}
}

public class TicketOrderItem {
	public TicketOrder TicketOrder { get; set; } = default!;
	public TicketType TicketType { get; set; } = default!;
	public int Quantity { get; set; }
}
