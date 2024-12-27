using Microsoft.AspNetCore.Mvc;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ControllerInterfaces;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;
using pigeon_lib.Utils;

namespace pigeon_crud_service.Controllers;

[ApiController, Route($"api/v1/pigeon-crud/[controller]/[action]")]
public class FirmController : ControllerBase, IController<Firm>
{
	private readonly IService<Firm> firmService;

	public FirmController(IService<Firm> firmService)
	{
		this.firmService = firmService;
	}

	[HttpGet(nameof(Get))]
	public ActionResult<Firm> Get(Guid id)
	{
		var firm = firmService.Get(id);
		if (firm == null)
		{
			return NotFound();
		}
		return Ok(firm);
	}

	[HttpGet(nameof(GetList))]
	public ActionResult<List<Firm>> GetList()
	{
		var firmList = firmService.GetList();
		return Ok(firmList);
	}

	/// <summary>
	/// Key-Value Dictionary would not let filtering two or more for same entity id parameter.
	/// To achieve this, arguing filterParams object must be detailed more.
	/// </summary>
	/// <param name="filterParams"></param>	
	/// <returns></returns>
	[HttpGet(nameof(Filter))]
	public ActionResult<List<Firm>> Filter(IFilterParams filterParams)
	{
		var list = firmService.Filter(filterParams);
		return Ok(list);
	}

	[HttpPost(nameof(Post))]
	public ActionResult<ReactedResult<Firm>> Post(Firm t)
	{
		return firmService.Post(t);
	}

	[HttpPut(nameof(Put))]
	public ActionResult<ReactedResult<Firm>> Put(Firm t)
	{
		return firmService.Put(t);
	}

	[HttpDelete(nameof(Delete))]
	public ActionResult<ReactedResult<Firm>> Delete(Guid id)
	{
		return firmService.Delete(id);
	}
}
