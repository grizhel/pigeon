using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using dotnet_third_party_integrations_core.kafka.models;
using dotnet_third_party_integrations_core.Kafka;
using Microsoft.AspNetCore.Http.Extensions;
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
		private readonly PigeonCrudDBContext dbContext;
		private readonly KafkaOptions kafkaOptions;
		private readonly bool isKafkaRunning;

		public ContactService(PigeonCrudDBContext dbContext, IOptions<AppOptions> appOptionsCarrier, IOptions<KafkaOptions> kafkaOptionsCarrier) : base(appOptionsCarrier)
		{
			this.dbContext = dbContext;
			this.kafkaOptions = kafkaOptionsCarrier.Value;
			this.isKafkaRunning = Hermes.IsRunning(kafkaOptionsCarrier.Value);
		}

		public async Task<Contact?> GetAsync(Guid id)
		{
			return await dbContext.Contacts.FirstOrDefaultAsync(q => q.ContactId == id);
		}

		public async Task<Contact?> GetDetailsAsync(Guid contactId)
		{
			return await dbContext.Contacts
					.Include(q => q.ContactInformations)
					.Include(q => q.Firm).ThenInclude(f => f.Location)
					.Include(q => q.Location)
					.FirstOrDefaultAsync(q => q.ContactId == contactId);
		}

		public async Task<List<Contact>> GetListAsync()
		{
			return [.. await dbContext.Contacts.Take(_limitList).ToListAsync()];
		}

		public async Task<List<Contact>> FilterParamsAsync(IFilterParams filterParams, bool quick)
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
																		.Where(f => f.ContactType == ContactTypes.Location && f.ContactInformationId == locationId).Select(t => t.ContactInformationId)
																		.Contains(q.ContactId));
				}
				else
				{
					throw new Exception("Location Id UUID formatında değil veya yanlış.");
				}
			}

			var searchString = filterParams.Params.GetValueOrDefault("search");

			if (searchString != null)
			{
				queryBuilder = createFilterForString(searchString, quick, queryBuilder);
			}

			queryBuilder = queryBuilder.OrderBy(q => q.Name).ThenBy(q => q.Surname);

			if (!quick)
			{
				queryBuilder.OrderBy(q => q.ContactInformations.FirstOrDefault(f => f.ContactType == ContactTypes.Phone).Value)
				.ThenBy(q => q.Firm.Name).ThenBy(q => q.Firm.Location.Name)
				.ThenBy(q => q.Firm.Location.Address)
				.ThenBy(q => q.Firm.Location.NVIAddress);
			}

			return [.. await queryBuilder.Take(_limitList).ToListAsync()];
		}

		private IQueryable<Contact> createFilterForString(string searchString, bool quick, IQueryable<Contact>? queryBuilder = null)
		{
			if (string.IsNullOrWhiteSpace(searchString))
			{
				throw new ArgumentNullException($"{nameof(searchString)} must have at least one character.");
			}

			searchString = searchString.ToLower();

			queryBuilder ??= dbContext.Contacts.AsQueryable();

			queryBuilder = queryBuilder.Where(q => q.Name.Contains(searchString) || q.Surname.Contains(searchString));

			return queryBuilder;
		}

		public async Task<List<Contact>> FilterStringAsync(string searchString, bool quick = false)
		{
			var queryBuilder = createFilterForString(searchString, quick);

			queryBuilder = queryBuilder.OrderBy(q => q.Name).ThenBy(q => q.Surname);

			if (!quick)
			{
				queryBuilder = queryBuilder
				.OrderBy(q => q.ContactInformations.SelectMany(q => q.Value))
				.ThenBy(q => q.Firm.Name).ThenBy(q => q.Firm.Location.Name)
				.ThenBy(q => q.Firm.Location.Address)
				.ThenBy(q => q.Firm.Location.NVIAddress);
			}
			return [.. await queryBuilder.Take(_limitList).ToListAsync()];
		}

		public async Task<ReactedResult<Contact>> PostAsync(Contact contact)
		{
			await dbContext.Contacts.AddAsync(contact);
			await dbContext.SaveChangesAsync();
			if (isKafkaRunning) 
			{
				await Hermes.SendMessageAsync(kafkaOptions, KafkaTopics.ContactIsCreated.ToString(), contact);
			}
			return ReactedResult<Contact>.Successful(contact);
		}

		public async Task<ReactedResult<Contact>> PutAsync(Contact contact)
		{
			var contactEntity = await dbContext.Contacts.FirstOrDefaultAsync(q => q.ContactId == contact.ContactId);
			dbContext.Contacts.Update(contact);
			await dbContext.SaveChangesAsync();
			if (isKafkaRunning)
			{
				await Hermes.SendMessageAsync(kafkaOptions, KafkaTopics.ContactIsUpdated.ToString(), contact);
			}
			return ReactedResult<Contact>.Successful(contact);
		}

		public async Task<ReactedResult<Contact>> DeleteAsync(Guid id)
		{
			var contact = await dbContext.Contacts.FirstOrDefaultAsync(q => q.ContactId == id);
			if (contact == null)
			{
				return ReactedResult<Contact>.Failed(HttpStatusCode.NotFound, $"There is not any Contact with Id of {id}");
			}
			dbContext.Contacts.Remove(contact);
			await dbContext.SaveChangesAsync();
			if (isKafkaRunning)
			{
				await Hermes.SendMessageAsync(kafkaOptions, KafkaTopics.ContactIsRemoved.ToString(), contact);
			}
			return ReactedResult<Contact>.Successful(contact);
		}

	}
}
