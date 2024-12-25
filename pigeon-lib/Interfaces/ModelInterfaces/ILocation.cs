namespace pigeon_lib.Models.Interfaces.ModelInterfaces;

public interface ILocation
{
	Guid Id { get; set; }

	string Name { get; set; }

	string NVIAddress { get; set; }

	string Address { get; set; }
}
