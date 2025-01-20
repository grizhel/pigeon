using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pigeon_lib.Enums;
using pigeon_lib.Interfaces.ModelInterfaces;

namespace pigeon_report_service.Models.DBModels
{
	[Table(nameof(Report))]
	public class Report : IReport
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid ReportId { get; set; }

		[Column(TypeName = "varchar(128)")]
		public required string Name { get; set; } = DefaultReports.SystematicLocationalReport.ToString();

		public Dictionary<string, string> Details { get; set; } = new Dictionary<string, string>
						{
							{ "Location", "Ankara" },
							{ "NumberOfContact", "0" },
							{ "NumberOfContactWithPhoneNumber", "0" }
						};

		public DateTime RequestDate { get; set; } = DateTime.Now;

		public DateTime? DueDate { get; set; }

		public DateTime? CompleteDate { get; set; }

		public ReportStatus ReportStatus { get; set; } = ReportStatus.InProgress;
	}
}


/*
Rapor basitçe aşağıdaki bilgileri içerecektir:

    Konum Bilgisi
    O konumda yer alan rehbere kayıtlı kişi sayısı
    O konumda yer alan rehbere kayıtlı telefon numarası sayısı

Veri yapısı olarak da:

    UUID
    Raporun Talep Edildiği Tarih
    Rapor Durumu (Hazırlanıyor, Tamamlandı)

*/