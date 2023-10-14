using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rockaway.WebApp.Data.Entities;

public class Artist {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
	[MaxLength(500)]
	public string Description { get; set; } = String.Empty;

	[MaxLength(100)]
	[Unicode(false)]
	[RegularExpression("^[a-z0-9-]{2,100}$", ErrorMessage = "Slug must be 2-100 characters and contain only a-z, 0-9 and hyphen (-) characters")]
	public string Slug { get; set; } = String.Empty;

	public Artist() { }

	public Artist(Guid id, string name, string description, string slug) {
		this.Id = id;
		this.Name = name;
		this.Description = description;
		this.Slug = slug;
	}
}