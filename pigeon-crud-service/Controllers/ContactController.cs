using Microsoft.AspNetCore.Mvc;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ControllerInterfaces;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;

namespace pigeon_crud_service.Controllers;

public class ContactController : ControllerBase, IController<Contact>
{
	private readonly IService<Contact> _contactService;

	public ContactController(IService<Contact> contactService)
	{
		_contactService = contactService;
	}

	public ActionResult<Contact> Get(Guid id)
	{
		var contact = _contactService.Get(id);
		if (contact == null) 
		{
			return NotFound();
		}
		return Ok(contact);
	}

	public ActionResult<List<Contact>> GetList()
	{
		var contactList = _contactService.GetList();
		return Ok(contactList);
	}

	/// <summary>
	/// Key-Value Dictionary would not let filtering two or more for same entity id parameter.
	/// To achieve this, arguing filterParams object must be detailed more.
	/// </summary>
	/// <param name="filterParams"></param>	
	/// <returns></returns>
	public ActionResult<List<Contact>> Filter(IFilterParams filterParams)
	{
		var list = _contactService.Filter(filterParams);
		return Ok(list);
	}

	public ActionResult<ReactedResult<Contact>> Post(Contact t)
	{
		return _contactService.Post(t);
	}

	public ActionResult<ReactedResult<Contact>> Put(Contact t)
	{
		return _contactService.Put(t);
	}

	public ActionResult<ReactedResult<Contact>> Delete(Guid id)
	{
		return _contactService.Delete(id);
	}
}
