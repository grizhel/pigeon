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
		private readonly PigeonCrudDBContext dbContext;

		public LocationService(PigeonCrudDBContext dbContext, IOptions<AppOptions> appOptions) : base(appOptions)
		{
			this.dbContext = dbContext;
		}

		public async Task<Location?> GetAsync(Guid id)
		{
			return await dbContext.Locations.FirstOrDefaultAsync(q => q.LocationId == id);
		}

		public async Task<Location?> GetDetailsAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Location>> GetListAsync()
		{
			return [.. await dbContext.Locations.Take(_limitList).ToListAsync()];
		}
		
		public Task<List<Location>> FilterStringAsync(string searchString, bool quick = false)
		{
			throw new NotImplementedException();
		}

		public Task<List<Location>> FilterParamsAsync(IFilterParams filterParams, bool quick = false)
		{
			throw new NotImplementedException();
		}

		public async Task<ReactedResult<Location>> PostAsync(Location location)
		{
			await dbContext.Locations.AddAsync(location);
			await dbContext.SaveChangesAsync();
			return ReactedResult<Location>.Successful(location);
		}

		public async Task<ReactedResult<Location>> PutAsync(Location location)
		{
			var locationEntity = await dbContext.SaveChangesAsync(); dbContext.Locations.FirstOrDefaultAsync(q => q.LocationId == location.LocationId);
			dbContext.Locations.Update(location);
			await dbContext.SaveChangesAsync();
			return ReactedResult<Location>.Successful(location);
		}

		public async Task<ReactedResult<Location>> DeleteAsync(Guid id)
		{
			var locationEntity = await dbContext.Locations.FirstOrDefaultAsync(q => q.LocationId == id);
			if (locationEntity == null)
			{
				return ReactedResult<Location>.Failed(HttpStatusCode.NotFound, $"There is not any Location with Id of {id}");
			}
			dbContext.Locations.Remove(locationEntity);
			await dbContext.SaveChangesAsync();
			return ReactedResult<Location>.Successful(locationEntity);
		}
	}
}
