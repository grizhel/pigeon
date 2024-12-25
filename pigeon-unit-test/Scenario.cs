using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Enums;

namespace pigeon_unit_test;

/// <summary>
/// ChatGPT created the example scenario
/// </summary>
public static class Scenario
{
	public static dynamic CreateDbScenario()
	{
		// Initialize some sample data for Locations and Firms
		var locations = Enumerable.Range(1, 5).Select(i => new Location
		{
			Id = Guid.NewGuid(),
			Name = $"Location-{i}",
			NVIAddress = $"NVI-{i}",
			Address = $"Address-{i}"
		}).ToList();

		var firms = Enumerable.Range(1, 10).Select(i => new Firm
		{
			Id = Guid.NewGuid(),
			Name = $"Firm-{i}",
			LocationId = locations[i % locations.Count].Id,
			Location = locations[i % locations.Count]
		}).ToList();

		// Generate 50 contacts
		var random = new Random();
		var contacts = Enumerable.Range(1, 50).Select(i => new Contact
		{
			Id = Guid.NewGuid(),
			Name = $"Name-{i}",
			Surname = $"Surname-{i}",
			FirmId = firms[i % firms.Count].Id,
			Firm = firms[i % firms.Count],
			ContactInformations = GenerateRandomContactInfos(random, i)
		}).ToList();

		return new { locations, contacts, firms, contactInfos = contacts.SelectMany(q=>q.ContactInformations) };
	}

	private static List<ContactInfo> GenerateRandomContactInfos(Random random, int contactIndex)
	{
		var contactInfoCount = random.Next(1, 4); // Each contact can have 1 to 3 contact types
		var contactInfos = new List<ContactInfo>();

		var availableTypes = Enum.GetValues(typeof(ContactTypes)).Cast<ContactTypes>().ToList();

		for (int i = 0; i < contactInfoCount; i++)
		{
			var contactType = availableTypes[random.Next(availableTypes.Count)];
			availableTypes.Remove(contactType); // Ensure no duplicate types for this contact

			contactInfos.Add(new ContactInfo
			{
				Id = Guid.NewGuid(),
				ContactId = Guid.NewGuid(), // Placeholder; in real cases, use the Contact's Id
				ContactType = contactType,
				Info = $"{contactType}-Info-{contactIndex}-{i + 1}"
			});
		}

		return contactInfos;
	}
}
