using System;
using System.Collections;
using pigeon_lib.Enums;

namespace pigeon_lib.Models.Interfaces.ModelInterfaces;

public interface IContactInfo
{
	Guid Id { get; set; }

	Guid ContactId { get; set; }

	ContactTypes ContactType { get; set; }

	string Info { get; set; }
}
