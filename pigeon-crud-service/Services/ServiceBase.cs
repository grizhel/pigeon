using Microsoft.Extensions.Options;
using pigeon_crud_service.Utils;

namespace pigeon_crud_service.Services
{
	public class ServiceBase
	{
		internal readonly int _limitList;
		public ServiceBase(IOptions<IAppOptions> appOptions)
		{
			_limitList = appOptions.Value.ListLimit;
		}
	}
}
