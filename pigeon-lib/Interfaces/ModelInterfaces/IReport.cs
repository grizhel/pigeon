using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pigeon_lib.Interfaces.ModelInterfaces
{
	public interface IReport
	{
		Guid Id { get; set; }

		string Name { get; set; }

		Guid LocationId { get; set; }
	}
}
