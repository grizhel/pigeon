using Microsoft.AspNetCore.Mvc;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ControllerInterfaces;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;
using pigeon_lib.Utils;

namespace pigeon_crud_service.Controllers;

[ApiController, Route($"api/v1/pigeon-crud/[controller]/")]
public class LocationController : ControllerBase, IController<Location>
{
	private readonly IService<Location> locationService;

	public LocationController(IService<Location> locationService)
	{
		this.locationService = locationService;
	}

	[HttpGet]
	public async Task<ActionResult<Location>> GetAsync(Guid id)
	{
		var location = await locationService.GetAsync(id);
		if (location == null)
		{
			return NotFound();
		}
		return Ok(location);
	}

	[HttpGet]
	public Task<ActionResult<Location?>> GetDetailsAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	[HttpGet]
	public async Task<ActionResult<List<Location>>> GetListAsync()
	{
		var locationList = await locationService.GetListAsync();
		return Ok(locationList);
	}

	/// <summary>
	/// Key-Value Dictionary would not let filtering two or more for same entity id parameter.
	/// To achieve this, arguing filterParams object must be detailed more.
	/// </summary>
	/// <param name="filterParams"></param>	
	/// <returns></returns>
	[HttpGet]
	public async Task<ActionResult<List<Location>>> FilterParamsAsync(IFilterParams filterParams, bool quick = false)
	{
		throw new NotImplementedException();
	}

	[HttpGet]
	public async Task<ActionResult<List<Location>>> FilterStringAsync(string searchString, bool quick = false)
	{
		throw new NotImplementedException();
	}

	[HttpPost]
	public async Task<ActionResult<ReactedResult<Location>>> PostAsync(Location t)
	{
		throw new NotImplementedException();
	}

	[HttpPut]
	public async Task<ActionResult<ReactedResult<Location>>> PutAsync(Location t)
	{
		throw new NotImplementedException();
	}

	[HttpDelete]
	public async Task<ActionResult<ReactedResult<Location>>> DeleteAsync(Guid id)
	{
		throw new NotImplementedException();
	}
}
