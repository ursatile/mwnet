using System.ComponentModel.DataAnnotations;

namespace Rockaway.WebApp.Data.Entities;

public class Artist {
	public Guid Id { get; set; }

	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;

	public string Description { get; set; } = String.Empty;
}
