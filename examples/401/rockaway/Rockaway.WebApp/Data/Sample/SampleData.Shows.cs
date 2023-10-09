namespace Rockaway.WebApp.Data.Sample; 

public static partial class SampleData {

	public static class Shows {
		public static Show Astoria20230901 =
			Venues.Astoria.BookShow(Artists.DevLeppard, new(2023, 09, 01))
				.WithSupportActs(Artists.BodyBag, Artists.KillerBite);

		public static Show Astoria20230902 =
			Venues.Astoria.BookShow(Artists.HaskellsAngels, new(2023, 09, 02))
				.WithSupportActs(Artists.Coda, Artists.Yamb);

		private static readonly IEnumerable<Show> shows = new[] {
			Astoria20230901, Astoria20230902
		};

		public static IEnumerable<object> AllShows
			=> shows.Select(show => show.ToSeedData());

		public static IEnumerable<object> AllSupportSlots
			=> shows.SelectMany(s => s.SupportActs).Select(act => act.ToSeedData());
	}
}