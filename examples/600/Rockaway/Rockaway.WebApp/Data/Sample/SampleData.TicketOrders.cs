using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Data.Sample;

public static partial class SampleData {

	public static class TicketOrders {

		private static int seed = 1;
		private static Guid NextId => TestGuid(seed++, 'D');

		public static TicketOrder Order001 =
			Shows.Coda_Barracuda_20240517.CreateTestOrder(NextId, "Ace Frehley", "ace@example.com");

		public static TicketOrder Order002 =
			Shows.Coda_NewCrossInn_20240520.CreateTestOrder(NextId, "Brian Johnson", "brian@example.com");

		public static TicketOrder Order003 =
			Shows.Coda_PubAnchor_20240523.CreateTestOrder(NextId, "Joey Tempest", "joey.tempest@example.com");

		public static IEnumerable<TicketOrder> AllTicketOrders = [Order001, Order002, Order003];

		public static IEnumerable<TicketOrderItem> AllTicketOrderItems
			=> AllTicketOrders.SelectMany(o => o.Contents);
	}


	public static TicketOrder CreateTestOrder(this Show show, Guid id, string name, string email) {
		// We need a random, but stable, quantity for each item, between 1 and 6,
		// so we use the length of the ticket type name, modulo 5, plus 1.
		var quantities = show.TicketTypes.ToDictionary(tt => tt.Id, tt => 1 + tt.Name.Length % 5);
		var o = show.CreateOrder(quantities);
		o.CustomerEmail = email;
		o.CustomerName = name;
		o.CreatedAt = show.Date.AtMidnight().InUtc().PlusHours(-123).ToInstant();
		o.CreatedAt = show.Date.AtMidnight().InUtc().PlusHours(-123).PlusMinutes(2).ToInstant();
		o.Id = id;
		return o;
	}
}