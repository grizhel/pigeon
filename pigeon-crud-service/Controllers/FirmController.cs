using Microsoft.AspNetCore.Mvc;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ControllerInterfaces;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;

namespace pigeon_crud_service.Controllers;

public class FirmController : ControllerBase, IController<Firm>
{
	private readonly IService<Firm> _firmService;

	public FirmController(IService<Firm> firmService)
	{
		_firmService = firmService;
	}

	public ActionResult<Firm> Get(Guid id)
	{
		var firm = _firmService.Get(id);
		if (firm == null)
		{
			return NotFound();
		}
		return Ok(firm);
	}

	public ActionResult<List<Firm>> GetList()
	{
		var firmList = _firmService.GetList();
		return Ok(firmList);
	}

	/// <summary>
	/// Key-Value Dictionary would not let filtering two or more for same entity id parameter.
	/// To achieve this, arguing filterParams object must be detailed more.
	/// </summary>
	/// <param name="filterParams"></param>	
	/// <returns></returns>
	public ActionResult<List<Firm>> Filter(IFilterParams filterParams)
	{
		var list = _firmService.Filter(filterParams);
		return Ok(list);
	}

	public ActionResult<ReactedResult<Firm>> Post(Firm t)
	{
		return _firmService.Post(t);
	}

	public ActionResult<ReactedResult<Firm>> Put(Firm t)
	{
		return _firmService.Put(t);
	}

	public ActionResult<ReactedResult<Firm>> Delete(Guid id)
	{
		return _firmService.Delete(id);
	}
}
