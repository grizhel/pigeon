using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using pigeon_crud_service.Models;
using pigeon_crud_service.Models.DBModels;
using pigeon_crud_service.Services;
using pigeon_crud_service.Utils;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models.Interfaces.ModelInterfaces;

namespace pigeon_unit_test
{
	public class ContactTest
	{
		public dynamic PrepareEnvironment()
		{
			var dbContext = new Mock<PigeonDBContext>(); 
			AppOptions _appOptions = new() { ListLimit = 8 };
			var contactService = new Mock<ContactService>(dbContext, new Mock<IOptions<AppOptions>>(_appOptions));

			var data = Scenario.CreateDbScenario();


			var mockSetContact = new Mock<DbSet<Contact>>();
			mockSetContact.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => data.contacts.GetEnumerator());

			var mockSetFirm = new Mock<DbSet<Firm>>();
			mockSetFirm.As<IQueryable<Firm>>().Setup(m => m.GetEnumerator()).Returns(() => data.firms.GetEnumerator());

			var mockSetLocation = new Mock<DbSet<Location>>();
			mockSetLocation.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(() => data.locations.GetEnumerator());


			dbContext.Setup(m => m.Contacts).Returns(mockSetContact.Object);
			dbContext.Setup(m => m.Firms).Returns(mockSetFirm.Object);
			dbContext.Setup(m => m.Locations).Returns(mockSetLocation.Object);

			var _contactService = contactService.Object;
			var _dbContext = dbContext.Object;

			return new { service = _contactService, dbContext = _dbContext, data };
		}

		public dynamic GetEnvironmet()
		{
			var preps = PrepareEnvironment();
			var service = (IService<Contact>)preps.service;
			var dbContext = (PigeonDBContext)preps.dbContext;
			var data = preps.data;
			var contactData = (List<Contact>)data.contacts;
			var firmData = (List<Firm>)data.firms;
			var locationData = (List<Location>)data.locations;
			var contactInfoData = (List<ContactInfo>)data.contactInfos;
			return new { service, dbContext, contactData, firmData, locationData, contactInfoData };
		}


		[TestCase]
		public void GetTest()
		{
			var env = GetEnvironmet();
			var passWithId = env.contactData.First().Id;
			Assert.That(env.service.Get(passWithId) != null);

			var failWithId = Guid.NewGuid();
			Assert.That(env.service.Get(failWithId) == null);
		}
	}
}
