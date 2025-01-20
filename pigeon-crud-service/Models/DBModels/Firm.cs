﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using pigeon_lib.Models.Interfaces.ModelInterfaces;

namespace pigeon_crud_service.Models.DBModels;

[Table(nameof(Firm))]
public class Firm : IFirm
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid FirmId { get; set; }

    [Column(TypeName = "varchar(128)")]
    public required string Name { get; set; }

    public Guid? LocationId { get; set; }

    public Location? Location { get; set; }

    public ICollection<Contact>? Contacts { get; set; }
}
