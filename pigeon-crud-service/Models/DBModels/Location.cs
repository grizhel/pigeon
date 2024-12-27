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
	public Guid Id { get; set; }

	[Column(TypeName = "varchar(64)")]
	public required string Name { get; set; }

	public string NVIAddress { get; set; }

	public string Address { get; set; }

	public LocationType LocationType { get; set; }
}
