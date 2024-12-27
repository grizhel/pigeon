using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pigeon_report_service.Models
{
	[Table(nameof(Info))]
	public class Info : IInfo
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public Dictionary<string, string>? Details { get; set; }

		public InfoType InfoType { get; set; }
	}
}
