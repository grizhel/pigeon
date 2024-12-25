using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using pigeon_lib.Models.Interfaces.ModelInterfaces;

namespace pigeon_crud_service.Models.DBModels;

[Table(nameof(Firm))]
public class Firm : IFirm
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(100)")]
    public required string Name { get; set; }

    public Guid LocationId { get; set; }

    public Location? Location { get; set; }

    public ICollection<Contact>? Contacts { get; set; }
}
