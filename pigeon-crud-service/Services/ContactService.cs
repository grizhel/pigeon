using System.Dynamic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using pigeon_crud_service.Models;
using pigeon_crud_service.Models.DBModels;
using pigeon_crud_service.Utils;
using pigeon_lib.Enums;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;

namespace pigeon_crud_service.Services
{
	public class ContactService : ServiceBase, IService<Contact>
	{
		private readonly PigeonDBContext _dbContext;

		public ContactService(PigeonDBContext dbContext, IOptions<IAppOptions> appOptions) : base(appOptions)
		{
			_dbContext = dbContext;
		}

		public Contact? Get(Guid id)
		{
			// FirstOrDefault is used for time efficiency.
			return _dbContext.Contacts.FirstOrDefault(q => q.Id == id);
		}

		public List<Contact> GetList()
		{
			return [.. _dbContext.Contacts.Take(_limitList)];
		}

		public List<Contact> Filter(IFilterParams filterParams)
		{
			var firmIdStr = filterParams.Params.GetValueOrDefault("firmId");
			
			var queryBuilder = _dbContext.Contacts.AsQueryable();
			if (firmIdStr != null) 
			{
				if (Guid.TryParse(firmIdStr, out var firmId))
					{
					queryBuilder = queryBuilder.Where(q => q.FirmId == firmId);
				}
				else
				{
					throw new Exception("Firma Id UUID formatında değil veya yanlış.");
				}
			}

			var locationIdStr = filterParams.Params.GetValueOrDefault("locationId");

			if (firmIdStr != null)
			{
				if (Guid.TryParse(firmIdStr, out var locationId))
				{
					queryBuilder = queryBuilder
															.Where(q => q.ContactInformations != null && q.ContactInformations
																		.Where(f => f.ContactType == ContactTypes.Location && f.Id == locationId).Select(t=> t.Id)
																		.Contains(q.Id));
				}
				else
				{
					throw new Exception("Location Id UUID formatında değil veya yanlış.");
				}
			}

			return [.. queryBuilder.Take(_limitList)];
		}

		public ReactedResult<Contact> Post(Contact contact)
		{
			_dbContext.Contacts.Add(contact);
			_dbContext.SaveChanges();
			return ReactedResult<Contact>.Successful(contact);
		}

		public ReactedResult<Contact> Put(Contact contact)
		{
			var contactEntity =  _dbContext.Contacts.FirstOrDefault(q => q.Id == contact.Id);
			_dbContext.Contacts.Update(contact);
			_dbContext.SaveChanges();
			return ReactedResult<Contact>.Successful(contact);
		}

		public ReactedResult<Contact> Delete(Guid id)
		{
			var contactEntity = _dbContext.Contacts.FirstOrDefault(q => q.Id == id);
			if(contactEntity == null)
			{
				return ReactedResult<Contact>.Failed(HttpStatusCode.NotFound, $"There is not any Contact with Id of {id}");
			}
			_dbContext.Contacts.Remove(contactEntity);
			_dbContext.SaveChanges();
			return ReactedResult<Contact>.Successful(contactEntity);
		}
	}
}
