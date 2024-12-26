using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pigeon_lib.Models.Interfaces.ModelInterfaces;

namespace pigeon_crud_service.Models.DBModels;

[Table(nameof(Contact))]
public class Contact : IContact
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	[Column(TypeName = "varchar(63)")]
	public required string Name { get; set; }

	[Column(TypeName = "varchar(63)")]
	public required string Surname { get; set; }

	[ForeignKey(nameof(IFirm))]
	public Guid? FirmId { get; set; }

	public Firm? Firm { get; set; }

	public ICollection<ContactInfo>? ContactInformations { get; set; }
}
