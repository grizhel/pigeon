using System.Dynamic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using pigeon_crud_service.Models;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.BaseModels;
using pigeon_lib.Enums;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;
using pigeon_lib.Utils;

namespace pigeon_crud_service.Services
{
	public class LocationService : ServiceBase, IService<Location>
	{
		private readonly PigeonDBContext dbContext;

		public LocationService(PigeonDBContext dbContext, IOptions<AppOptions> appOptions) : base(appOptions)
		{
			this.dbContext = dbContext;
		}

		public Location? Get(Guid id)
		{
			// FirstOrDefault is used for time efficiency.
			return dbContext.Locations.FirstOrDefault(q => q.Id == id);
		}

		public List<Location> GetList()
		{
			return [.. dbContext.Locations.Take(_limitList)];
		}

		public List<Location> Filter(IFilterParams filterParams)
		{
			throw new NotImplementedException(@"
								Locations are not filtered. This method is implemented for more complex filtering which is unnecesary at the moment
								");
		}

		public ReactedResult<Location> Post(Location location)
		{
			dbContext.Locations.Add(location);
			dbContext.SaveChanges();
			return ReactedResult<Location>.Successful(location);
		}

		public ReactedResult<Location> Put(Location location)
		{
			var locationEntity = dbContext.Locations.FirstOrDefault(q => q.Id == location.Id);
			dbContext.Locations.Update(location);
			dbContext.SaveChanges();
			return ReactedResult<Location>.Successful(location);
		}

		public ReactedResult<Location> Delete(Guid id)
		{
			var locationEntity = dbContext.Locations.FirstOrDefault(q => q.Id == id);
			if (locationEntity == null)
			{
				return ReactedResult<Location>.Failed(HttpStatusCode.NotFound, $"There is not any Location with Id of {id}");
			}
			dbContext.Locations.Remove(locationEntity);
			dbContext.SaveChanges();
			return ReactedResult<Location>.Successful(locationEntity);
		}
	}
}
