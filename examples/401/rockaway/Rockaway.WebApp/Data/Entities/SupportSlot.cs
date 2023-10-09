namespace Rockaway.WebApp.Data.Entities;

public class SupportSlot {
	public Show Show { get; set; } = null!;
	public int SlotNumber { get; set; }
	public Artist Artist { get; set; } = null!;
	public string Comments { get; set; } = String.Empty;
}
