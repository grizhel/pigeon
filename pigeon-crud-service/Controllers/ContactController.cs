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
	public async Task<ActionResult<Contact?>> GetAsync(Guid id)
	{
		var contact = await contactService.GetAsync(id);
		if (contact == null) 
		{
			return NotFound();
		}
		return Ok(contact);
	}

	public async Task<ActionResult<Contact?>> GetDetailsAsync(Guid id)
	{
		return await contactService.GetDetailsAsync(id);
	}

	[HttpGet]
	public async Task<ActionResult<List<Contact>>> GetListAsync()
	{
		var contactList = await contactService.GetListAsync();
		return Ok(contactList);
	}

	/// <summary>
	/// Key-Value Dictionary would not let filtering two or more for same entity id parameter.
	/// To achieve this, arguing filterParams object must be detailed more.
	/// </summary>
	/// <param name="filterParams"></param>	
	/// <returns></returns>
	[HttpGet]
	public async Task<ActionResult<List<Contact>>> FilterParamsAsync(IFilterParams filterParams, bool quick = false)
	{
		var list = await contactService.FilterParamsAsync(filterParams);
		return Ok(list);
	}

	[HttpGet]
	public async Task<ActionResult<List<Contact>>> FilterStringAsync(string searchString, bool quick = false)
	{
		var list = await contactService.FilterStringAsync(searchString, quick);
		return Ok(list);
	}

	[HttpPost]
	public async Task<ActionResult<ReactedResult<Contact>>> PostAsync(Contact t)
	{
		return Ok(await contactService.PostAsync(t));
	}

	[HttpPut]
	public async Task<ActionResult<ReactedResult<Contact>>> PutAsync(Contact t)
	{
		return Ok(await contactService.PutAsync(t));
	}

	[HttpDelete]
	public async Task<ActionResult<ReactedResult<Contact>>> DeleteAsync(Guid id)
	{
		return Ok(await contactService.DeleteAsync(id));
	}
}
