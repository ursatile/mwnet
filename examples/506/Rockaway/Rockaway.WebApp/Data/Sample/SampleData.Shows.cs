using Rockaway.WebApp.Data.Entities;
// ReSharper disable InconsistentNaming

namespace Rockaway.WebApp.Data.Sample;

public partial class SampleData {

	public static class Shows {
		public static readonly Show Coda_Barracuda_20240517 = Venues.Barracuda
			.BookShow(Artists.Coda, new(2024, 5, 17))
			.WithSupportActs(Artists.KillerBite, Artists.Overflow);

		public static readonly Show Coda_Columbia_20240518 = Venues.Columbia
			.BookShow(Artists.Coda, new(2024, 5, 18))
				.WithSupportActs(Artists.KillerBite, Artists.Overflow);

		public static readonly Show Coda_Bataclan_20240519 = Venues.Bataclan
			.BookShow(Artists.Coda, new(2024, 5, 19))
				.WithSupportActs(Artists.KillerBite, Artists.Overflow, Artists.JavasCrypt);


		public static readonly Show Coda_NewCrossInn_20240520 = Venues.NewCrossInn
			.BookShow(Artists.Coda, new(2024, 5, 20))
				.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_JohnDee_20240522 = Venues.JohnDee
			.BookShow(Artists.Coda, new(2024, 5, 22))
				.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_PubAnchor_20240523 = Venues.PubAnchor
			.BookShow(Artists.Coda, new(2024, 5, 23))
				.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_Gagarin_20240525 =
			Venues.Gagarin.BookShow(Artists.Coda, new(2024, 5, 25))
				.WithSupportActs(Artists.JavasCrypt, Artists.SilverMountainStringBand);

		public static IEnumerable<Show> AllShows = new[] {
			Coda_Barracuda_20240517, Coda_Columbia_20240518, Coda_Bataclan_20240519, Coda_NewCrossInn_20240520, Coda_JohnDee_20240522, Coda_PubAnchor_20240523, Coda_Gagarin_20240525
		};


		public static IEnumerable<SupportSlot> AllSupportSlots
			=> AllShows.SelectMany(show => show.SupportSlots);
	}
}
