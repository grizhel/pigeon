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
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
using pigeon_lib.Utils;
using pigeon_report_service.Models;
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

		public MainTest(IOptions<AppOptions> options)
		{
			options.Value.ListLimit = 8;
			contactService = new(dbContext, options, null);
			firmService = new(dbContext, options);
			locationService = new(dbContext, options);
		}

		/// <summary>
		/// 1st Requirement: Create contact (Rehberde kişi oluşturma).
		/// </summary>
		/// <param name="contact"></param>
		public void CreateContact(Contact contact)
    {
      contactService.Post(contact);
		}

		/// <summary>
		/// 2nd Requirement: Remove Contact (Rehberde kişi kaldırma).
		/// </summary>
		/// <param name="contactId"></param>
		public void DeleteContact(Guid contactId)
		{

		}

		/// <summary>
		/// 3rd Requirement: Add information to a spesific contact (Rehberdeki kişiye iletişim bilgisi ekleme).
		/// 4th Requirement: Remove information from a spesific contact (Rehberdeki kişiden iletişim bilgisi kaldırma).
		/// </summary>
		/// <param name="contact"></param>
		public void UpdateContact(Contact contact) 
    {
      contactService.Put(contact);
    }

		/// <summary>
		/// 5th Requirement: List contacts (Rehberdeki kişilerin listelenmesi).
		/// </summary>
		public void GetContactList()
		{

		}

		/// <summary>
		/// 6th Requirement: Get detailed information of a specific contact 
		/// (Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi).
		/// </summary>
		/// <param name="contactId"></param>
		public void GetContactDetails(Guid contactId)
		{

		}

		/// <summary>
		/// 7th Requirement: Get location based statistics of the contacts
		/// (Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi).
		/// </summary>
		public void GetSystematicLocationalReport() 
		{
			Report systematicLocationalReport = reportService.GetSystematicLocationalReport();
		}

		/// <summary>
		/// 8th Requirement: Listing reports (Sistemin oluşturduğu raporların listelenmesi).
		/// </summary>
		public void ListReports()
		{
			List<Report> systematicLocationalReport = reportService.GetList();
		}

		/// <summary>
		/// 9th Requirement: Get detailed report (Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi).
		/// </summary>
		/// <param name="reportId"></param>
		public void GetDetailedReport(Guid reportId)
		{
			Report systematicLocationalReport = reportService.GetDetailedReport(reportId);
		}
	}
}


/*
    
    
    
    
    
    
    
    
    
 
*/

/*
Rapor basitçe aşağıdaki bilgileri içerecektir:

    Konum Bilgisi
    O konumda yer alan rehbere kayıtlı kişi sayısı
    O konumda yer alan rehbere kayıtlı telefon numarası sayısı

Veri yapısı olarak da:

    UUID
    Raporun Talep Edildiği Tarih
    Rapor Durumu (Hazırlanıyor, Tamamlandı)

*/

/*

    Projenin sık commitlerle Git üzerinde geliştirilmesi
    Git üzerinde master, development branchleri ve sürüm taglemelerinin kullanımı
    Minimum %60 unit testing code coverage
    Projenin veritabanını oluşturacak migration yapısının oluşturulmuş olması
    Projenin nasıl çalıştırılacağına dair README.md dokümantasyonu
    Servislerin HTTP üzerinden REST veya GraphQL protokolleri üzerinden iletişimi sağlanmalı
    Rapor kısmındaki asenkron yapının sağlanması için message queue gibi sistemler kullanılmalıdır
*/