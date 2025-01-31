﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pigeon_lib.Models.Interfaces.ModelInterfaces;

public interface IContact
{
	Guid ContactId { get; set; }

	string Name { get; set; }

	string Surname { get; set; }

	Guid? FirmId { get; set; }
}
