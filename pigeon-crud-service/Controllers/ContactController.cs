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

	[HttpGet]
	public async Task<ActionResult<Contact>> GetAsync(Guid id)
	{
		var contact = contactService.GetAsync(id);
		if (contact == null) 
		{
			return NotFound();
		}
		return Ok(contact);
	}

	[HttpGet]
	public async Task<ActionResult<List<Contact>>> GetListAsync()
	{
		var contactList = contactService.GetListAsync();
		return Ok(contactList);
	}

	/// <summary>
	/// Key-Value Dictionary would not let filtering two or more for same entity id parameter.
	/// To achieve this, arguing filterParams object must be detailed more.
	/// </summary>
	/// <param name="filterParams"></param>	
	/// <returns></returns>
	[HttpGet]
	public async Task<ActionResult<List<Contact>>> FilterAsync(IFilterParams filterParams)
	{
		var list = await contactService.FilterAsync(filterParams);
		return Ok(list);
	}

	[HttpPost]
	public async Task<ActionResult<ReactedResult<Contact>>> PostAsync(Contact t)
	{
		return await contactService.PostAsync(t);
	}

	[HttpPut]
	public async Task<ActionResult<ReactedResult<Contact>>> PutAsync(Contact t)
	{
		return await contactService.PutAsync(t);
	}

	[HttpDelete]
	public async Task<ActionResult<ReactedResult<Contact>>> DeleteAsync(Guid id)
	{
		return await contactService.DeleteAsync(id);
	}
}
