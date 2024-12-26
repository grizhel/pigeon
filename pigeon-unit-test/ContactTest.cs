using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using pigeon_crud_service.Models;
using pigeon_crud_service.Models.DBModels;
using pigeon_crud_service.Services;
using pigeon_crud_service.Utils;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
using Xunit;

namespace pigeon_unit_test
{
	public class ContactTest : TestBase
	{
		private readonly ContactService contactService;

		public ContactTest(IOptions<IAppOptions> options)
		{
			options.Value.ListLimit = 8;
			contactService = new(dbContext, options);
		}


		[Fact]
		public void GetTest()
		{
			var datum = data.contacts.First();
			Assert.True(contactService.Get(datum.Id) != null);
			Assert.Equals(contactService.Get(datum.Id), datum);

			var failWithId = Guid.NewGuid();
			Assert.True(contactService.Get(failWithId) == null);
		}
	}
}
