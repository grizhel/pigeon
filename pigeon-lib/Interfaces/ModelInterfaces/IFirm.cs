namespace pigeon_lib.Models.Interfaces.ModelInterfaces;

public interface IFirm
{
	Guid Id { get; set; }

	string Name { get; set; }

	Guid LocationId { get; set; }
}
