using System;
using System.Collections;
using pigeon_lib.Enums;

namespace pigeon_lib.Models.Interfaces.ModelInterfaces;

public interface IContactInformation
{
	Guid Id { get; set; }

	ContactTypes ContactType { get; set; }

	Guid ContactId { get; set; }

	string Value { get; set; }
}
