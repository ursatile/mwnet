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
		// To generate random-but-stable data, we use numbers based on
		// the modulo of various string properties.
		var quantities = show.TicketTypes.ToDictionary(tt => tt.Id, tt => 1 + tt.Name.Length % 5);
		var createdAt = show.Date.AtMidnight().InUtc().Minus(Duration.FromDays(42))
			.PlusHours(show.Venue.Name.Length)
			.PlusMinutes(show.HeadlineArtist.Name.Length)
			.PlusSeconds(show.Venue.Address.Length)
			.ToInstant();
		var o = show.CreateOrder(quantities, createdAt);
		o.CustomerEmail = email;
		o.CustomerName = name;
		o.CompletedAt = createdAt.Plus(Duration.FromMinutes(show.Venue.FullAddress.Length));
		o.Id = id;
		return o;
	}
}