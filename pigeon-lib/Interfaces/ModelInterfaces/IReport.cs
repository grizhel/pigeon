using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pigeon_lib.Enums;

namespace pigeon_lib.Interfaces.ModelInterfaces
{
	public interface IReport
	{
		Guid ReportId { get; set; }

		string Name { get; set; }

		DateTime RequestDate { get; set; }

		DateTime? DueDate { get; set; }

		DateTime? CompleteDate { get; set; }

		Dictionary<string, string>? Details { get; set; }

		ReportStatus ReportStatus { get; set; }
	}
}
