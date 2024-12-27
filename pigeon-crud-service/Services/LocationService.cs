using System.Dynamic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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

		public async Task<Location?> GetAsync(Guid id)
		{
			return await dbContext.Locations.FirstOrDefaultAsync(q => q.Id == id);
		}

		public async Task<List<Location>> GetListAsync()
		{
			return [.. await dbContext.Locations.Take(_limitList).ToListAsync()];
		}

		public async Task<List<Location>> FilterAsync(IFilterParams filterParams)
		{
			throw new NotImplementedException(@"
								Locations are not filtered. This method is implemented for more complex filtering which is unnecesary at the moment
								");
		}

		public async Task<ReactedResult<Location>> PostAsync(Location location)
		{
			await dbContext.Locations.AddAsync(location);
			await dbContext.SaveChangesAsync();
			return ReactedResult<Location>.Successful(location);
		}

		public async Task<ReactedResult<Location>> PutAsync(Location location)
		{
			var locationEntity = dbContext.Locations.FirstOrDefault(q => q.Id == location.Id);
			dbContext.Locations.Update(location);
			dbContext.SaveChanges();
			return ReactedResult<Location>.Successful(location);
		}

		public async Task<ReactedResult<Location>> DeleteAsync(Guid id)
		{
			var locationEntity = await dbContext.Locations.FirstOrDefaultAsync(q => q.Id == id);
			if (locationEntity == null)
			{
				return ReactedResult<Location>.Failed(HttpStatusCode.NotFound, $"There is not any Location with Id of {id}");
			}
			dbContext.Locations.Remove(locationEntity);
			dbContext.SaveChangesAsync();
			return ReactedResult<Location>.Successful(locationEntity);
		}
	}
}
