using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using pigeon_lib.Models.Interfaces.ModelInterfaces;

namespace pigeon_crud_service.Models.DBModels;

[Table(nameof(Contact))]
public class Contact : IContact
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid ContactId { get; set; }

	[Column(TypeName = "varchar(64)")]
	public required string Name { get; set; }

	[Column(TypeName = "varchar(64)")]
	public required string Surname { get; set; }

	[ForeignKey(nameof(IFirm))]
	public Guid? FirmId { get; set; }

	[ForeignKey(nameof(Location))]
	public Guid LocationId { get; set; }

	public Firm? Firm { get; set; }

	public Location? Location { get; set; }

	public ICollection<ContactInformation>? ContactInformations { get; set; }
}
