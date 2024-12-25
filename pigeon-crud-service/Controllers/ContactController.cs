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

	public ActionResult<List<Contact>> Filter(IFilterParams filterParams)
	{
		throw new NotImplementedException();
	}

	public ActionResult<ReactedResult<Contact>> Post(Contact t)
	{
		throw new NotImplementedException();
	}

	public ActionResult<ReactedResult<Contact>> Put(Contact t)
	{
		throw new NotImplementedException();
	}

	public ActionResult<ReactedResult<Contact>> Delete(Guid id)
	{
		throw new NotImplementedException();
	}
}
