using Microsoft.AspNetCore.Mvc;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ControllerInterfaces;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;
using pigeon_lib.Utils;

namespace pigeon_crud_service.Controllers;

[ApiController, Route($"api/v1/pigeon-crud/[controller]/")]
public class FirmController : ControllerBase, IController<Firm>
{
	private readonly IService<Firm> firmService;

	public FirmController(IService<Firm> firmService)
	{
		this.firmService = firmService;
	}

	[HttpGet]
	public async Task<ActionResult<Firm?>> GetAsync(Guid id)
	{
		var firm = await firmService.GetAsync(id);
		if (firm == null)
		{
			return NotFound();
		}
		return Ok(firm);
	}

	public Task<ActionResult<Firm?>> GetDetailsAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	[HttpGet]
	public async Task<ActionResult<List<Firm>>> GetListAsync()
	{
		var firmList = await firmService.GetListAsync();
		return Ok(firmList);
	}

	/// <summary>
	/// Key-Value Dictionary would not let filtering two or more for same entity id parameter.
	/// To achieve this, arguing filterParams object must be detailed more.
	/// </summary>
	/// <param name="filterParams"></param>	
	/// <returns></returns>
	[HttpGet]
	public async Task<ActionResult<List<Firm>>> FilterParamsAsync(IFilterParams filterParams, bool quick = false)
	{
		var list = await firmService.FilterParamsAsync(filterParams, quick);
		return Ok(list);
	}

	[HttpGet]
	public async Task<ActionResult<List<Firm>>> FilterStringAsync(string seachParam, bool quick = false)
	{
		var list = await firmService.FilterStringAsync(seachParam, quick);
		return Ok(list);
	}

	[HttpPost]
	public async Task<ActionResult<ReactedResult<Firm>>> PostAsync(Firm t)
	{
		throw new NotImplementedException();
	}

	[HttpPut]
	public async Task<ActionResult<ReactedResult<Firm>>> PutAsync(Firm t)
	{
		throw new NotImplementedException();
	}

	[HttpDelete]
	public async Task<ActionResult<ReactedResult<Firm>>> DeleteAsync(Guid id)
	{
		throw new NotImplementedException();
	}
}
