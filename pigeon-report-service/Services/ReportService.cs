using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using pigeon_lib.BaseModels;
using pigeon_lib.Enums;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
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

		public async Task<Report> GetDetailedReportAsync(Guid reportId)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Report>> GetListAsync()
		{
			return [.. await dBContext.Reports.Take(_limitList).ToListAsync()];
		}

		public async Task<Report> GetSystematicLocationalReportAsync()
		{
			throw new NotImplementedException();
		}

		public async Task ContactIsAdded(IContact? contact)
		{
			var info = await dBContext.Info.FirstOrDefaultAsync(q => q.InfoType == InfoType.CountOfContacts);
			if (info == null)
			{
				dBContext.Info.Add(new()
				{
					InfoType = InfoType.CountOfContacts,
					Details = new Dictionary<string, string>
				{
					{ "Count", "1" }
				}
				});
			}
			else
			{
				info.Details["Count"] = (int.Parse(info.Details["Count"]) + 1).ToString();
				dBContext.Info.Update(info);
			}
			await dBContext.SaveChangesAsync();
		}

		public async Task ContactIsRemoved(IContact? contact)
		{
			var info = dBContext.Info.FirstOrDefault(q => q.InfoType == InfoType.CountOfContacts);
			if(info == null)
			{
				throw new Exception($"{nameof(ReportService)} - {nameof(ContactIsRemoved)} - a contact cannot remove if there is none in the database. Dirty Data.");
			}
			info.Details["Count"] = (int.Parse(info.Details["Count"]) - 1).ToString();
			dBContext.Info.Update(info);
			await dBContext.SaveChangesAsync();
		}

		public void ContactIsUpdated(IContact? contact)
		{
			// Other reports??
		}
	}
}
