using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Moq;
using Npgsql;
using System;
using System.Data.Common;
using System.Data;
using Xunit;
using dotnet_third_party_integrations_core.utils;
using System.Text.Json;
using Microsoft.Extensions.Options;
using pigeon_crud_service.Models;
using pigeon_report_service.Models;
//using initReportMig = pigeon_report_service.Migrations.init;
//using initCrudMig = pigeon_crud_service.Migrations.init;
//using Microsoft.EntityFrameworkCore.Migrations;
using pigeon_crud_service.Models.DBModels;
using Microsoft.Data.Sqlite;
using pigeon_crud_service.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pigeon_report_service.Models.DBModels;


namespace pigeon_unit_test
{
	public class TestBase : IDisposable
	{

		public readonly PigeonCrudDBContext pigeonCrudDBContext;
		public readonly PigeonReportDBContext pigeonReportDBContext;
		public (List<Contact>, List<Location>, List<Firm>, List<ContactInformation>) data;
		public bool disposedValue;
		public readonly List<Contact> contacts;
		public readonly List<Firm> firms;
		public readonly List<Location> locations;
		public readonly List<ContactInformation> contactInformations;

		protected TestBase()
		{
			data = Scenario.CreateDbScenario();
			contacts = data.Item1;
			locations = data.Item2;
			firms = data.Item3;
			contactInformations = data.Item4;



			var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

			//var connection = new SqliteConnection("DataSource=:memory:");
			//connection.Open();

			//var optionsCrud = new DbContextOptionsBuilder<PigeonCrudDBContext>().UseSqlite(connection).Options;

			//using (var context = new PigeonCrudDBContext(optionsCrud, configuration))
			//{
			//	Assert.NotNull(context);
			//	pigeonCrudDBContext = context;
			//	pigeonCrudDBContext!.Database.EnsureCreated();
			//}

			//var optionsReport = new DbContextOptionsBuilder<PigeonReportDBContext>().UseSqlite(connection).Options;

			//using (var context = new PigeonReportDBContext(optionsReport, configuration))
			//{
			//	Assert.NotNull(context);
			//	pigeonReportDBContext = context;
			//	pigeonReportDBContext!.Database.EnsureCreated();
			//}



			var builder1 = new DbContextOptionsBuilder<PigeonCrudDBContext>();
			builder1.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
			pigeonCrudDBContext = new PigeonCrudDBContext(builder1.Options, configuration);
			pigeonCrudDBContext.Database.EnsureCreated();
			pigeonCrudDBContext.Database.ExecuteSqlRaw("create schema if not exists test authorization pigeon;");
			pigeonCrudDBContext.Database.ExecuteSqlRaw($"create table if not exists pigeon_v1.test.\"{nameof(Location)}\" as select * from pigeon_v1.crud.\"{nameof(Location)}\"");
			pigeonCrudDBContext.Database.ExecuteSqlRaw($"create table if not exists pigeon_v1.test.\"{nameof(Firm)}\" as select * from pigeon_v1.crud.\"{nameof(Firm)}\"");
			pigeonCrudDBContext.Database.ExecuteSqlRaw($"create table if not exists pigeon_v1.test.\"{nameof(ContactInformation)}\" as select * from pigeon_v1.crud.\"{nameof(ContactInformation)}\"");
			pigeonCrudDBContext.Database.ExecuteSqlRaw($"create table if not exists pigeon_v1.test.\"{nameof(Contact)}\" as select * from pigeon_v1.crud.\"{nameof(Contact)}\"");
			pigeonCrudDBContext.Database.EnsureCreated();
			pigeonCrudDBContext.Set<Location>($"pigeon_v1.test.\"{nameof(Location)}\"");
			pigeonCrudDBContext.Set<Firm>($"pigeon_v1.test.\"{nameof(Firm)}");
			pigeonCrudDBContext.Set<ContactInformation>($"pigeon_v1.test.\"{nameof(ContactInformation)}\"");
			pigeonCrudDBContext.Set<Contact>($"pigeon_v1.test.\"{nameof(Contact)}\"");
			pigeonCrudDBContext.AddRange(contacts);
			pigeonCrudDBContext.AddRange(locations);
			pigeonCrudDBContext.AddRange(firms);
			pigeonCrudDBContext.AddRange(contactInformations);
			pigeonCrudDBContext.SaveChanges();

			var builder2 = new DbContextOptionsBuilder<PigeonReportDBContext>();
			builder2.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
			pigeonReportDBContext = new PigeonReportDBContext(builder2.Options, configuration);
			pigeonReportDBContext.Database.EnsureCreated();
			pigeonReportDBContext.Database.ExecuteSqlRaw($"create table if not exists pigeon_v1.test.\"{nameof(Report)}\" as select * from pigeon_v1.report.\"{nameof(Report)}\"");
			pigeonReportDBContext.Database.ExecuteSqlRaw($"create table if not exists pigeon_v1.test.\"{nameof(Info)}\" as select * from pigeon_v1.report.\"{nameof(Info)}\"");
			pigeonReportDBContext.Set<Report>($"pigeon_v1.test.\"{nameof(Report)}\"");
			pigeonReportDBContext.Set<Info>($"pigeon_v1.test.\"{nameof(Info)}\"");
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					Console.WriteLine("Disposing");
				}

				Console.WriteLine("Disposed");

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			//pigeonCrudDBContext.Database.EnsureDeleted();
			//pigeonReportDBContext.Database.EnsureDeleted();
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}

