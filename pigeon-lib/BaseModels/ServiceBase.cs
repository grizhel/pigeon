using Microsoft.Extensions.Options;
using pigeon_lib.Utils;

namespace pigeon_lib.BaseModels
{
	public class ServiceBase
	{
		public readonly int _limitList;
		public ServiceBase(IOptions<AppOptions> appOptions)
		{
			_limitList = appOptions.Value.ListLimit;
		}		
	}
}


