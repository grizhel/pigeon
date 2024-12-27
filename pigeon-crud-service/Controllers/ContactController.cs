using Microsoft.AspNetCore.Mvc;
using pigeon_crud_service.Models.DBModels;
using pigeon_crud_service.Services;
using pigeon_lib.Interfaces.ControllerInterfaces;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Utils;

namespace pigeon_crud_service.Controllers;

[ApiController, Route($"api/v1/pigeon-crud/[controller]/[action]")]
public class ContactController : ControllerBase, IController<Contact>
{
	private readonly IService<Contact> contactService;

	public ContactController(ContactService contactService)
	{
		this.contactService = contactService;
	}

	[HttpGet(nameof(Get))]
	public ActionResult<Contact> Get(Guid id)
	{
		var contact = contactService.Get(id);
		if (contact == null) 
		{
			return NotFound();
		}
		return Ok(contact);
	}

	[HttpGet(nameof(GetList))]
	public ActionResult<List<Contact>> GetList()
	{
		var contactList = contactService.GetList();
		return Ok(contactList);
	}

	/// <summary>
	/// Key-Value Dictionary would not let filtering two or more for same entity id parameter.
	/// To achieve this, arguing filterParams object must be detailed more.
	/// </summary>
	/// <param name="filterParams"></param>	
	/// <returns></returns>
	[HttpGet(nameof(Filter))]
	public ActionResult<List<Contact>> Filter(IFilterParams filterParams)
	{
		var list = contactService.Filter(filterParams);
		return Ok(list);
	}

	[HttpPost(nameof(Post))]
	public ActionResult<ReactedResult<Contact>> Post(Contact t)
	{
		return contactService.Post(t);
	}

	[HttpPut(nameof(Put))]
	public ActionResult<ReactedResult<Contact>> Put(Contact t)
	{
		return contactService.Put(t);
	}

	[HttpDelete(nameof(Delete))]
	public ActionResult<ReactedResult<Contact>> Delete(Guid id)
	{
		return contactService.Delete(id);
	}
}
