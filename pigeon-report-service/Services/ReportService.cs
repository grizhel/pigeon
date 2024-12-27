using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using pigeon_lib.BaseModels;
using pigeon_lib.Utils;
using pigeon_report_service.Models;

namespace pigeon_report_service.Services
{
	public class ReportService : ServiceBase
	{
		private readonly PigeonReportDBContext dBContext;

		public ReportService(PigeonReportDBContext dbContext, IOptions<AppOptions> appOptions) : base(appOptions)
		{
			dBContext = dbContext;
		}

		public Report GetDetailedReport(Guid reportId)
		{
			throw new NotImplementedException();
		}

		public List<Report> GetList()
		{
			return [.. dBContext.Reports.Take(_limitList)];
		}

		public Report GetSystematicLocationalReport()
		{
			throw new NotImplementedException();
		}
	}
}
