using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text.Json;
using dotnet_third_party_integrations_core.kafka.models;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using pigeon_crud_service.Models;
using pigeon_crud_service.Models.DBModels;
using pigeon_crud_service.Services;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
using pigeon_lib.Utils;
using pigeon_report_service.Models.DBModels;
using pigeon_report_service.Services;
using Xunit;

namespace pigeon_unit_test
{
	public class MainTest : TestBase
	{
		private readonly ContactService contactService;
		private readonly FirmService firmService;
		private readonly LocationService locationService;
		private readonly ReportService reportService;
		private readonly bool isKafkaRunning;
		private readonly KafkaOptions kafkaOptions;

		[assembly: DisableTestParallelization(true)] // DBContext is connected to real database.
		public MainTest()
		{
			kafkaOptions = JsonSerializer.Deserialize<KafkaOptions>(@"
			{
					""BootstrapServers"": ""localhost:9092"",
					""KafkaConfig"": {
						""AutoOffsetReset"": ""Earliest"",
						""EnableAutoCommit"": ""true"",
						""EnableAutoOffsetStore"": ""false"",
						""GroupId"": ""PigeonApp"",
						""SessionTimeoutMs"": ""6000"",
						""StatisticsIntervalMs"": ""5000"",
						""AllowAutoCreateTopics"": ""true""
					}
				}
			")!;

			var appOptions = JsonSerializer.Deserialize<AppOptions>(@"
			{
				""ListLimit"": 512
			}");

			var appOptionsCarrier = Options.Create(appOptions!);
			var kafkaOptionsCarrier = Options.Create(kafkaOptions!);

			contactService = new(pigeonCrudDBContext, appOptionsCarrier!, kafkaOptionsCarrier!);
			reportService = new(pigeonReportDBContext, appOptionsCarrier!);
			firmService = new(pigeonCrudDBContext, appOptionsCarrier!);
			locationService = new(pigeonCrudDBContext, appOptionsCarrier!);
			isKafkaRunning = kafkaOptions!.IsRunning();
		}

		/// <summary>
		/// 1st Requirement: Create contact (Rehberde kişi oluşturma).
		/// </summary>
		/// <param name="contact"></param>
		[Fact]
		public async Task CreateContactAsync()
		{
			var contact = contacts.First();
			await contactService.PostAsync(contact);
			Assert.True(kafkaOptions.IsRunning());
			List<Info> infoList = await reportService.GetInfoListAsync();
			Assert.True(infoList.Count > 0);
			Equals(contact, contacts.FirstOrDefault());
		}

		/// <summary>
		/// 2nd Requirement: Remove Contact (Rehberde kişi kaldırma).
		/// </summary>
		/// <param name="contactId"></param>
		[Fact]
		public async Task DeleteContactAsync()
		{
			var contactPreviousCount = (await contactService.GetListAsync()).Count;
			var contact = contacts.First();
			var res = contactService.DeleteAsync(contact.ContactId).Result;
			Assert.True(res.Success);
			Assert.True(kafkaOptions.IsRunning());
			Assert.True(pigeonCrudDBContext.Contacts.Any(q => q.ContactId != contact.ContactId));
		}

		/// <summary>
		/// 3rd Requirement: Add information to a spesific contact (Rehberdeki kişiye iletişim bilgisi ekleme).
		/// 4th Requirement: Remove information from a spesific contact (Rehberdeki kişiden iletişim bilgisi kaldırma).
		/// </summary>
		/// <param name="contact"></param>
		[Fact]
		public async Task UpdateContactAsync()
		{
			var contactId = (await contactService.GetListAsync()).Select(q => q.ContactId).First();
			var contact = (await contactService.GetListAsync()).First();
			var contactPreviousName = JsonSerializer.Serialize(contact.Name); // DeepCopy
			var contactNewName =  contactId.ToString();
			contact.Name = contactNewName;
			await contactService.PutAsync(contact);
			var contactPreviousCount = (await contactService.GetListAsync()).Count;
			Assert.True(kafkaOptions.IsRunning());
			Assert.True((await contactService.GetListAsync()).Count == contactPreviousCount);
			Assert.True(pigeonCrudDBContext.Contacts.Any(q => q.Name == contactNewName));
			Assert.False(pigeonCrudDBContext.Contacts.Any(q => q.ContactId == contactId && q.Name == contactPreviousName));
		}

		/// <summary>
		/// 5th Requirement: List contacts (Rehberdeki kişilerin listelenmesi).
		/// </summary>
		[Fact]
		public async Task GetContactListAsync()
		{
			Equals(await contactService.GetListAsync(), contacts);
		}

		/// <summary>
		/// 6th Requirement: Get detailed information of a specific contact 
		/// (Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi).
		/// </summary>
		/// <param name="contactId"></param>
		[Fact]
		public async Task GetDetailsAsync()
		{
			var contactList = await contactService.GetListAsync();
			foreach (var contactId in contacts.Select(q=>q.ContactId))
			{
				Equals((await contactService.GetDetailsAsync(contactId))!.FirmId == contacts.Single(q => q.ContactId == contactId).FirmId);
				Equals((await contactService.GetDetailsAsync(contactId))!.LocationId == contacts.Single(q => q.ContactId == contactId).LocationId);
			}
		}

		/// <summary>
		/// 7th Requirement: Get location based statistics of the contacts
		/// (Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi).
		/// </summary>
		[Fact]
		public async Task GetSystematicLocationalReportAsync()
		{
			var locationList = await locationService.GetListAsync();
			var contactList = await contactService.GetListAsync();
			Equals(Location.GetSystematicLocationalReport(locations, contacts), Location.GetSystematicLocationalReport(pigeonCrudDBContext.Locations.ToList(), pigeonCrudDBContext.Contacts.ToList()));
		}

		/// <summary>
		/// 8th Requirement: Listing reports (Sistemin oluşturduğu raporların listelenmesi).
		/// </summary>
		[Fact]
		public async Task ListReportAsync()
		{
		}

		/// <summary>
		/// 9th Requirement: Get detailed report (Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi).
		/// </summary>
		/// <param name="reportId"></param>
		[Fact]
		public async Task GetDetailedReport()
		{
		}
	}
}