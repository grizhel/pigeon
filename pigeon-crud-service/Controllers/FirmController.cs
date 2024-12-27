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
	public async Task<ActionResult<Firm>> GetAsync(Guid id)
	{
		var firm = await firmService.GetAsync(id);
		if (firm == null)
		{
			return NotFound();
		}
		return Ok(firm);
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
	public async Task<ActionResult<List<Firm>>> FilterAsync(IFilterParams filterParams)
	{
		var list = await firmService.FilterAsync(filterParams);
		return Ok(list);
	}

	[HttpPost]
	public async Task<ActionResult<ReactedResult<Firm>>> PostAsync(Firm t)
	{
		return await firmService.PostAsync(t);
	}

	[HttpPut]
	public async Task<ActionResult<ReactedResult<Firm>>> PutAsync(Firm t)
	{
		return await firmService.PutAsync(t);
	}

	[HttpDelete]
	public async Task<ActionResult<ReactedResult<Firm>>> DeleteAsync(Guid id)
	{
		return await firmService.DeleteAsync(id);
	}
}
