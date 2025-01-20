﻿using System.Dynamic;
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
	public class FirmService : ServiceBase, IService<Firm>
	{
		private readonly PigeonCrudDBContext dbContext;

		public FirmService(PigeonCrudDBContext dbContext, IOptions<AppOptions> appOptions) : base(appOptions)
		{
			this.dbContext = dbContext;
		}

		public async Task<Firm?> GetAsync(Guid id)
		{
			return await dbContext.Firms.FirstOrDefaultAsync(q => q.FirmId == id);
		}

		public Task<Firm> GetDetailsAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Firm>> GetListAsync()
		{
			return [.. await dbContext.Firms.Take(_limitList).ToListAsync()];
		}
		
		public Task<List<Firm>> FilterStringAsync(string searchString, bool quick = false)
		{
			throw new NotImplementedException();
		}

		public Task<List<Firm>> FilterParamsAsync(IFilterParams filterParams, bool quick = false)
		{
			throw new NotImplementedException();
		}

		public async Task<ReactedResult<Firm>> PostAsync(Firm firm)
		{
			await dbContext.Firms.AddAsync(firm);
			await dbContext.SaveChangesAsync();
			return ReactedResult<Firm>.Successful(firm);
		}

		public async Task<ReactedResult<Firm>> PutAsync(Firm firm)
		{
			var firmEntity = await dbContext.Firms.FirstOrDefaultAsync(q => q.FirmId == firm.FirmId);
			dbContext.Firms.Update(firm);
			await dbContext.SaveChangesAsync();
			return ReactedResult<Firm>.Successful(firm);
		}

		public async Task<ReactedResult<Firm>> DeleteAsync(Guid id)
		{
			var firm = await dbContext.Firms.FirstOrDefaultAsync(q => q.FirmId == id);
			if (firm == null)
			{
				return ReactedResult<Firm>.Failed(HttpStatusCode.NotFound, $"There is not any Firm with Id of {id}");
			}
			dbContext.Firms.Remove(firm);
			await dbContext.SaveChangesAsync();
			return ReactedResult<Firm>.Successful(firm);
		}
	}
}
