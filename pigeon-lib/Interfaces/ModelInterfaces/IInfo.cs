using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pigeon_lib.Enums;

namespace pigeon_lib.Interfaces.ModelInterfaces
{
	public interface IInfo
	{
		Guid InfoId { get; set; }

		InfoType InfoType { get; set; }

		Dictionary<string, string>? Details { get; set; }
	}
}
