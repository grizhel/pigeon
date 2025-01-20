using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
using pigeon_lib.Enums;

namespace pigeon_crud_service.Models.DBModels;

[Table(nameof(Location))]
public class Location : ILocation
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid LocationId { get; set; }

	public required LocationType LocationType { get; set; }

	[Column(TypeName = "varchar(64)")]
	public required string Name { get; set; }

	[Column(TypeName = "varchar(16)")]
	public string? NVIAddress { get; set; }

	[Column(TypeName = "varchar(160)")]
	public string? Address { get; set; }

	public static Dictionary<string, int> GetSystematicLocationalReport(List<Location> locations, List<Contact> contacts)
	{
		Dictionary<string, int> locationContactCount = new Dictionary<string, int>();
		foreach (var location in locations)
		{
			var contactCount = contacts.Where(f => f.LocationId == location.LocationId).Count();
			if(locationContactCount.TryGetValue(location.Name, out contactCount))
			{
				locationContactCount[location.Name]++;
			}
			else
			{
				locationContactCount.Add(location.Name, contactCount);
			}
		}
		return locationContactCount;
	}
}
