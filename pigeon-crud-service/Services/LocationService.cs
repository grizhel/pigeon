using System.Dynamic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using pigeon_crud_service.Models;
using pigeon_crud_service.Models.DBModels;
using pigeon_crud_service.Utils;
using pigeon_lib.Enums;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;
using pigeon_lib.Utils;

namespace pigeon_crud_service.Services
{
	public class LocationService : ServiceBase, IService<Location>
	{
		private readonly PigeonDBContext _dbContext;

		public LocationService(PigeonDBContext dbContext, IOptions<IAppOptions> appOptions) : base(appOptions)
		{
			_dbContext = dbContext;
		}

		public Location? Get(Guid id)
		{
			// FirstOrDefault is used for time efficiency.
			return _dbContext.Locations.FirstOrDefault(q => q.Id == id);
		}

		public List<Location> GetList()
		{
			return [.. _dbContext.Locations.Take(_limitList)];
		}

		public List<Location> Filter(IFilterParams filterParams)
		{
			throw new NotImplementedException(@"
								Locations are not filtered. This method is implemented for more complex filtering which is unnecesary at the moment
								");
		}

		public ReactedResult<Location> Post(Location location)
		{
			_dbContext.Locations.Add(location);
			_dbContext.SaveChanges();
			return ReactedResult<Location>.Successful(location);
		}

		public ReactedResult<Location> Put(Location location)
		{
			var locationEntity = _dbContext.Locations.FirstOrDefault(q => q.Id == location.Id);
			_dbContext.Locations.Update(location);
			_dbContext.SaveChanges();
			return ReactedResult<Location>.Successful(location);
		}

		public ReactedResult<Location> Delete(Guid id)
		{
			var locationEntity = _dbContext.Locations.FirstOrDefault(q => q.Id == id);
			if (locationEntity == null)
			{
				return ReactedResult<Location>.Failed(HttpStatusCode.NotFound, $"There is not any Location with Id of {id}");
			}
			_dbContext.Locations.Remove(locationEntity);
			_dbContext.SaveChanges();
			return ReactedResult<Location>.Successful(locationEntity);
		}
	}
}
