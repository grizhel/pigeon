using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pigeon_lib.Interfaces.ModelInterfaces;

namespace pigeon_report_service.Models
{
	[Table(nameof(Report))]
	public class Report : IReport
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Column(TypeName = "varchar(128)")]
		public string Name { get; set; } = null!;
		
		public Dictionary<string,string>? Details { get; set; }
	}
}
