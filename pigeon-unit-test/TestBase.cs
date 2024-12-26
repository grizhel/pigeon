using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Npgsql;
using pigeon_crud_service.Models;
using pigeon_report_service.Models;
using System;


namespace pigeon_unit_test
{
	public class TestBase : IDisposable
	{

		protected readonly PigeonDBContext dbContext;
		protected readonly PigeonReportDBContext reportDbContext;
		protected readonly dynamic data;
		private bool disposedValue;

		protected TestBase()
		{
			data = Scenario.CreateDbScenario();
			dbContext = PrepareDatabase("EF-InMemory");
			reportDbContext = PrepareReportDatabase("EF-InMemory");
		}

		private PigeonDBContext PrepareDatabase(string databaseType)
		{
			DbContextOptions options;
			PigeonDBContext pigeonDBContext;
			switch (databaseType)
			{
				case "EF-InMemory":
					options = new DbContextOptionsBuilder<PigeonDBContext>()
							.ConfigureWarnings(x => x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning))
							.UseInMemoryDatabase("PigeonDBContext")
							.Options;
					pigeonDBContext = new PigeonDBContext(options, null);

					pigeonDBContext.Database.EnsureDeleted();
					pigeonDBContext.Database.EnsureCreated();


					pigeonDBContext.Contacts.Add(data.contacts);
					pigeonDBContext.Firms.Add(data.firms);
					pigeonDBContext.Locations.Add(data.locations);
					pigeonDBContext.ContactInformations.Add(data.contactInformations);
					pigeonDBContext.SaveChanges();

					break;
				default:
					throw new Exception();
			}

			return pigeonDBContext;
		}

		private PigeonReportDBContext PrepareReportDatabase(string databaseType)
		{
			DbContextOptions options;
			PigeonReportDBContext pigeonDBContext;
			switch (databaseType)
			{
				case "EF-InMemory":
					options = new DbContextOptionsBuilder<PigeonReportDBContext>()
							.ConfigureWarnings(x => x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning))
							.UseInMemoryDatabase("PigeonDBContext")
							.Options;
					pigeonDBContext = new PigeonReportDBContext(options, null);

					pigeonDBContext.Database.EnsureDeleted();
					pigeonDBContext.Database.EnsureCreated();
					break;
				default:
					throw new Exception();
			}

			return pigeonDBContext;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~TemelTest()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}

