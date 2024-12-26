using Microsoft.AspNetCore.Mvc;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ControllerInterfaces;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;
using pigeon_lib.Utils;

namespace pigeon_crud_service.Controllers;

[ApiController, Route($"api/v1/{nameof(Location)}/[controller]/[action]")]
public class LocationController : ControllerBase, IController<Location>
{
	private readonly IService<Location> _locationService;

	public LocationController(IService<Location> locationService)
	{
		_locationService = locationService;
	}

	[HttpGet(nameof(Get))]
	public ActionResult<Location> Get(Guid id)
	{
		var location = _locationService.Get(id);
		if (location == null)
		{
			return NotFound();
		}
		return Ok(location);
	}

	[HttpGet(nameof(GetList))]
	public ActionResult<List<Location>> GetList()
	{
		var locationList = _locationService.GetList();
		return Ok(locationList);
	}

	/// <summary>
	/// Key-Value Dictionary would not let filtering two or more for same entity id parameter.
	/// To achieve this, arguing filterParams object must be detailed more.
	/// </summary>
	/// <param name="filterParams"></param>	
	/// <returns></returns>
	[HttpGet(nameof(Filter))]
	public ActionResult<List<Location>> Filter(IFilterParams filterParams)
	{
		var list = _locationService.Filter(filterParams);
		return Ok(list);
	}

	[HttpPost(nameof(Post))]
	public ActionResult<ReactedResult<Location>> Post(Location t)
	{
		return _locationService.Post(t);
	}

	[HttpPut(nameof(Put))]
	public ActionResult<ReactedResult<Location>> Put(Location t)
	{
		return _locationService.Put(t);
	}

	[HttpDelete(nameof(Delete))]
	public ActionResult<ReactedResult<Location>> Delete(Guid id)
	{
		return _locationService.Delete(id);
	}
}
