using Rockaway.WebApp.Data.Entities;
// ReSharper disable InconsistentNaming

namespace Rockaway.WebApp.Data.Sample;

public partial class SampleData {

	public static class Shows {
		public static readonly Show Coda_Barracuda_20240517 = Venues.Barracuda
			.BookShow(Artists.Coda, new(2024, 5, 17))
			.WithTicketType("Upstairs unallocated seating", price: 25, limit: 100)
			.WithTicketType("Downstairs standing", price: 25, limit: 120)
			.WithTicketType("Cabaret table (4 people)", price: 120, limit: 10)
			.WithSupportActs(Artists.KillerBite, Artists.Overflow);

		public static readonly Show Coda_Columbia_20240518 = Venues.Columbia
			.BookShow(Artists.Coda, new(2024, 5, 18))
			.WithTicketType("General Admission", price: 35)
			.WithTicketType("VIP Meet & Greet", price: 75, limit: 20)
			.WithSupportActs(Artists.KillerBite, Artists.Overflow);

		public static readonly Show Coda_Bataclan_20240519 = Venues.Bataclan
			.BookShow(Artists.Coda, new(2024, 5, 19))
			.WithTicketType("General Admission", price: 35)
			.WithTicketType("VIP Meet & Greet", price: 75)
			.WithSupportActs(Artists.KillerBite, Artists.Overflow, Artists.JavasCrypt);

		public static readonly Show Coda_NewCrossInn_20240520 = Venues.NewCrossInn
			.BookShow(Artists.Coda, new(2024, 5, 20))
			.WithTicketType("General Admission", price: 25)
			.WithTicketType("VIP Meet & Greet", price: 55, limit: 20)
			.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_JohnDee_20240522 = Venues.JohnDee
			.BookShow(Artists.Coda, new(2024, 5, 22))
			.WithTicketType("General Admission", price: 350)
			.WithTicketType("VIP Meet & Greet", price: 750, limit: 25)
			.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_PubAnchor_20240523 = Venues.PubAnchor
			.BookShow(Artists.Coda, new(2024, 5, 23))
			.WithTicketType("General Admission", price: 300)
			.WithTicketType("VIP Meet & Greet", price: 720, limit: 10)
			.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_Gagarin_20240525 =
			Venues.Gagarin.BookShow(Artists.Coda, new(2024, 5, 25))
			.WithTicketType("General Admission", 25)
			.WithSupportActs(Artists.JavasCrypt, Artists.SilverMountainStringBand);

		public static IEnumerable<Show> AllShows = [
			Coda_Barracuda_20240517, Coda_Columbia_20240518,
			Coda_Bataclan_20240519, Coda_NewCrossInn_20240520,
			Coda_JohnDee_20240522, Coda_PubAnchor_20240523,
			Coda_Gagarin_20240525
		];

		public static IEnumerable<TicketType> AllTicketTypes
			=> AllShows.SelectMany(show => show.TicketTypes);

		public static IEnumerable<SupportSlot> AllSupportSlots
			=> AllShows.SelectMany(show => show.SupportSlots);
	}
}