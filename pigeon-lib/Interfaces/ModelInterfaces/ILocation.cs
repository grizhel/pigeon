using pigeon_lib.Enums;

namespace pigeon_lib.Models.Interfaces.ModelInterfaces;

public interface ILocation
{
	Guid Id { get; set; }

	string Name { get; set; }

	string NVIAddress { get; set; }

	string Address { get; set; }

	LocationType LocationType { get; set; }
}
