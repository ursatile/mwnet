using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace Rockaway.WebApp.Data.Rockaway.WebApp.Data.ValueConverters;

public class NodaTimeConverters {
	public class LocalTimeConverter : ValueConverter<LocalTime, TimeSpan> {
		public LocalTimeConverter() :
			base(localTime => TimeSpan.FromTicks(localTime.TickOfDay),
				timeOnly => LocalTime.FromTicksSinceMidnight(timeOnly.Ticks)) { }
	}

	public class LocalDateTimeConverter : ValueConverter<LocalDateTime, DateTime> {
		public LocalDateTimeConverter() :
			base(localDate => localDate.ToDateTimeUnspecified(),
				dateTime => LocalDateTime.FromDateTime(dateTime)) { }
	}

	public class LocalDateConverter : ValueConverter<LocalDate, DateTime> {
		public LocalDateConverter() :
			base(localDate => localDate.ToDateTimeUnspecified(),
				dateTime => LocalDate.FromDateTime(dateTime)) { }
	}
}