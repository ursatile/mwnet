using System.ComponentModel.DataAnnotations.Schema;

namespace Rockaway.WebApp.Data.Entities;

public class Artist {
	public Guid Id { get; set; }

	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;

	[MaxLength(500)]
	public string Description { get; set; } = String.Empty;

	[MaxLength(100)]
	[Unicode(false)]
	[RegularExpression(@"^[a-z0-9][a-z0-9-]{0,98}[a-z0-9]$", ErrorMessage = "Slug must be 2-100 characters and can only contain letters a-z, digits 0-9, and hyphens. It cannot start or end with a hyphen.")]
	public string Slug { get; set; } = String.Empty;
}