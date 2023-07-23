using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Data;

public interface IDatabase {
	IEnumerable<Artist> Artists { get; set; }
}


