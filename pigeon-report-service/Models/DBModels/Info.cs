using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pigeon_report_service.Models.DBModels
{
	[Table(nameof(Info))]
	public class Info : IInfo
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid InfoId { get; set; }

		public required InfoType InfoType { get; set; }

		public required Dictionary<string, string>? Details { get; set; }
	}
}
