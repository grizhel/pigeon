using System.Dynamic;
using System.Linq;
using System.Net;
using dotnet_third_party_integrations_core.kafka.models;
using dotnet_third_party_integrations_core.Kafka;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using pigeon_crud_service.Models;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.BaseModels;
using pigeon_lib.Enums;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;
using pigeon_lib.Utils;

namespace pigeon_crud_service.Services
{
	public class ContactService : ServiceBase, IService<Contact>
	{
		private readonly PigeonDBContext dbContext;
		private readonly KafkaOptions kafkaOptions;

		public ContactService(PigeonDBContext dbContext, IOptions<AppOptions> appOptions, IOptions<KafkaOptions> kafkaOptions) : base(appOptions)
		{
			this.dbContext = dbContext;
			this.kafkaOptions = kafkaOptions.Value;
		}

		public async Task<Contact?> GetAsync(Guid id)
		{
			return await dbContext.Contacts.FirstOrDefaultAsync(q => q.Id == id);
		}

		public async Task<List<Contact>> GetListAsync()
		{
			return [.. await dbContext.Contacts.Take(_limitList).ToListAsync()];
		}

		public async Task<List<Contact>> FilterAsync(IFilterParams filterParams)
		{
			var firmIdStr = filterParams.Params.GetValueOrDefault("firmId");
			
			var queryBuilder = dbContext.Contacts.AsQueryable();
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

			return [.. await queryBuilder.Take(_limitList).ToListAsync()];
		}

		public async Task<ReactedResult<Contact>> PostAsync(Contact contact)
		{
			await dbContext.Contacts.AddAsync(contact);
			await dbContext.SaveChangesAsync();
			await  Hermes.SendMessageAsync(kafkaOptions, KafkaTopics.ContactIsCreated.ToString(), contact);
			return ReactedResult<Contact>.Successful(contact);
		}

		public async Task<ReactedResult<Contact>> PutAsync(Contact contact)
		{
			var contactEntity = await dbContext.Contacts.FirstOrDefaultAsync(q => q.Id == contact.Id);
			dbContext.Contacts.Update(contact);
			await dbContext.SaveChangesAsync();
			await Hermes.SendMessageAsync(kafkaOptions, KafkaTopics.ContactIsUpdated.ToString(), contact);
			return ReactedResult<Contact>.Successful(contact);
		}

		public async Task<ReactedResult<Contact>> DeleteAsync(Guid id)
		{
			var contact = await dbContext.Contacts.FirstOrDefaultAsync(q => q.Id == id);
			if(contact == null)
			{
				return ReactedResult<Contact>.Failed(HttpStatusCode.NotFound, $"There is not any Contact with Id of {id}");
			}
			dbContext.Contacts.Remove(contact);
			await dbContext.SaveChangesAsync();
			await Hermes.SendMessageAsync(kafkaOptions, KafkaTopics.ContactIsRemoved.ToString(), contact);
			return ReactedResult<Contact>.Successful(contact);
		}
	}
}
