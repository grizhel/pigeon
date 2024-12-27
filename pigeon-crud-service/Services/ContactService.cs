using System.Dynamic;
using System.Linq;
using System.Net;
using dotnet_third_party_integrations_core.kafka.models;
using dotnet_third_party_integrations_core.Kafka;
using Microsoft.AspNetCore.Http.HttpResults;
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

		public Contact? Get(Guid id)
		{
			return dbContext.Contacts.FirstOrDefault(q => q.Id == id);
		}

		public List<Contact> GetList()
		{
			return [.. dbContext.Contacts.Take(_limitList)];
		}

		public List<Contact> Filter(IFilterParams filterParams)
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

			return [.. queryBuilder.Take(_limitList)];
		}

		public ReactedResult<Contact> Post(Contact contact)
		{
			dbContext.Contacts.Add(contact);
			dbContext.SaveChanges();
			Hermes.SendMessageAsync(kafkaOptions,KafkaTopics.ContactIsCreated.ToString(), contact);
			return ReactedResult<Contact>.Successful(contact);
		}

		public ReactedResult<Contact> Put(Contact contact)
		{
			var contactEntity =  dbContext.Contacts.FirstOrDefault(q => q.Id == contact.Id);
			dbContext.Contacts.Update(contact);
			dbContext.SaveChanges();
			Hermes.SendMessageAsync(kafkaOptions, KafkaTopics.ContactIsUpdated.ToString(), contact);
			return ReactedResult<Contact>.Successful(contact);
		}

		public ReactedResult<Contact> Delete(Guid id)
		{
			var contact = dbContext.Contacts.FirstOrDefault(q => q.Id == id);
			if(contact == null)
			{
				return ReactedResult<Contact>.Failed(HttpStatusCode.NotFound, $"There is not any Contact with Id of {id}");
			}
			dbContext.Contacts.Remove(contact);
			dbContext.SaveChanges();
			Hermes.SendMessageAsync(kafkaOptions, KafkaTopics.ContactIsRemoved.ToString(), contact);
			return ReactedResult<Contact>.Successful(contact);
		}
	}
}
