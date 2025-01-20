using Microsoft.AspNetCore.Mvc;
using pigeon_report_service.Models;

namespace pigeon_report_service.Controllers
{
	[ApiController, Route($"api/v1/pigeon-report/[controller]/[action]")]
	public class ReportController
	{
		public ReportController() { }
	}
}
