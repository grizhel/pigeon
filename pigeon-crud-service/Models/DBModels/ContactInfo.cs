using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
using pigeon_lib.Enums;

namespace pigeon_crud_service.Models.DBModels;

[Table(nameof(ContactInfo))]
public class ContactInfo : IContactInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(IContact))]
    public Guid ContactId { get; set; }

    public ContactTypes ContactType { get; set; }

    public IContact? Contact { get; set; }
}
