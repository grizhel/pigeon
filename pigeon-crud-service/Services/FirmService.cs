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
	public class FirmService : ServiceBase, IService<Firm>
	{
		private readonly PigeonDBContext dbContext;

		public FirmService(PigeonDBContext dbContext, IOptions<AppOptions> appOptions) : base(appOptions)
		{
			this.dbContext = dbContext;
		}

		public Firm? Get(Guid id)
		{
			// FirstOrDefault is used for time efficiency.
			return dbContext.Firms.FirstOrDefault(q => q.Id == id);
		}

		public List<Firm> GetList()
		{
			return [.. dbContext.Firms.Take(_limitList)];
		}

		public List<Firm> Filter(IFilterParams filterParams)
		{
			var locationIdStr = filterParams.Params.GetValueOrDefault("locationId");

			var queryBuilder = dbContext.Firms.AsQueryable();
			if (locationIdStr != null)
			{
				if (Guid.TryParse(locationIdStr, out var locationId))
				{
					queryBuilder = queryBuilder.Where(q => q.Id == locationId);
				}
				else
				{
					throw new Exception("Firma Id UUID formatında değil veya yanlış.");
				}
			}

			return [.. queryBuilder.Take(_limitList)];
		}

		public ReactedResult<Firm> Post(Firm firm)
		{
			dbContext.Firms.Add(firm);
			dbContext.SaveChanges();
			return ReactedResult<Firm>.Successful(firm);
		}

		public ReactedResult<Firm> Put(Firm firm)
		{
			var firmEntity = dbContext.Firms.FirstOrDefault(q => q.Id == firm.Id);
			dbContext.Firms.Update(firm);
			dbContext.SaveChanges();
			return ReactedResult<Firm>.Successful(firm);
		}

		public ReactedResult<Firm> Delete(Guid id)
		{
			var firmEntity = dbContext.Firms.FirstOrDefault(q => q.Id == id);
			if (firmEntity == null)
			{
				return ReactedResult<Firm>.Failed(HttpStatusCode.NotFound, $"There is not any Firm with Id of {id}");
			}
			dbContext.Firms.Remove(firmEntity);
			dbContext.SaveChanges();
			return ReactedResult<Firm>.Successful(firmEntity);
		}
	}
}
