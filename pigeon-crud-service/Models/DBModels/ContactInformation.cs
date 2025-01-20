using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
using pigeon_lib.Enums;

namespace pigeon_crud_service.Models.DBModels;

[Table(nameof(ContactInformation))]
public class ContactInformation : IContactInformation
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid ContactInformationId { get; set; }

	[ForeignKey(nameof(Contact))]
	public Guid ContactId { get; set; }

	public required ContactTypes ContactType { get; set; }

	public required string Value { get; set; }

	public Contact? Contact { get; set; }
}
