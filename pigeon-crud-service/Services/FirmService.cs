using System.Dynamic;
using pigeon_crud_service.Models;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;

namespace pigeon_crud_service.Services
{
	public class FirmService : ServiceBase, IService<Firm>
	{
		private readonly PigeonDBContext _dbContext;

		public FirmService(IConfiguration configuration, PigeonDBContext dbContext) : base(configuration)
		{
			_dbContext = dbContext;
		}

		public Firm Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public List<Firm> GetList()
		{
			throw new NotImplementedException();
		}

		public List<Firm> Filter(IFilterParams filterParams)
		{
			throw new NotImplementedException();
		}

		public ReactedResult<Firm> Post(Firm t)
		{
			throw new NotImplementedException();
		}

		public ReactedResult<Firm> Put(Firm t)
		{
			throw new NotImplementedException();
		}
		public ReactedResult<Firm> Delete(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
