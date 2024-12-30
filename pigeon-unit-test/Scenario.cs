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
	public static (List<Contact>, List<Location>, List<Firm>, List<ContactInformation>) CreateDbScenario()
	{
		// Initialize some sample data for Locations and Firms
		var locations = Enumerable.Range(1, 5).Select(i => new Location
		{
			LocationId = Guid.NewGuid(),
			LocationType = i % 2 == 0 ? LocationType.Home : LocationType.Work,
			Name = $"Location-{i}",
			NVIAddress = $"NVI-{i}",
			Address = $"Address-{i}"
		}).ToList();

		var firms = Enumerable.Range(1, 10).Select(i => new Firm
		{
			FirmId = Guid.NewGuid(),
			Name = $"Firm-{i}",
			LocationId = locations[i % locations.Count].LocationId,
			Location = locations[i % locations.Count]
		}).ToList();

		// Generate 50 contacts
		var random = new Random();
		var contacts = Enumerable.Range(1, 50).Select(i => new Contact
		{
			ContactId = Guid.NewGuid(),
			Name = $"Name-{i}",
			Surname = $"Surname-{i}",
			FirmId = firms[i % firms.Count].FirmId,
			LocationId = locations[i % locations.Count].LocationId,
			Location = locations[i % locations.Count],
			Firm = firms[i % firms.Count],
			ContactInformations = GenerateRandomContactInfos(random, i)
		}).ToList();

		return (contacts, locations, firms, contacts.SelectMany(q => q.ContactInformations!).ToList());
	}

	private static List<ContactInformation> GenerateRandomContactInfos(Random random, int contactIndex)
	{
		var contactInfoCount = random.Next(1, 4); // Each contact can have 1 to 3 contact types
		var contactInfos = new List<ContactInformation>();

		var availableTypes = Enum.GetValues(typeof(ContactTypes)).Cast<ContactTypes>().ToList();

		for (int i = 0; i < contactInfoCount; i++)
		{
			var contactType = availableTypes[random.Next(availableTypes.Count)];
			availableTypes.Remove(contactType); // Ensure no duplicate types for this contact

			contactInfos.Add(new ContactInformation
			{
				ContactInformationId = Guid.NewGuid(),
				ContactId = Guid.NewGuid(), // Placeholder; in real cases, use the Contact's Id
				ContactType = contactType,
				Value = $"{contactType}-Info-{contactIndex}-{i + 1}",
			});
		}

		return contactInfos;
	}
}
