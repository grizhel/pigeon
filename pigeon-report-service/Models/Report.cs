using pigeon_lib.Interfaces.ModelInterfaces;

namespace pigeon_report_service.Models
{
	public class Report : IReport
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = null!;
		
		public Guid LocationId { get; set; }

		public Dictionary<string,string>? Details { get; set; }
	}
}
