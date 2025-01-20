# Pigeon Project Backend

## Notes

- Some features such as logging mechanism are not implemented.
- The Filter method in the services requires a custom model called IFilterParams as an argument.
- Kafka messages sent to the ReportService are designed in a fire-and-forget manner. If there are issues with these messages, the reports may be incorrect. However, this can be managed through a set of background services that will run overnight.
- Access to the ReportService via Kafka is limited to the ContactService. Information is sent to the ReportService whenever a contact is added, deleted, or updated.

# Input - Output

## Post Contact

```json
{
  "name": "name1",
  "surname": "surname1",
  "firm": {
    "name": "firm1",
    "location": {
      "name": "firm1street",
      "nviAddress": "123456",
      "address": "firm1address",
      "locationType": "Work"
    }
  },
  "contactInformations": [
    {
      "contactType": "Phone",
      "info": "+905559993636"
    },
    {
      "contactType": "Email",
      "info": "contac1@contacts.com"
    }
  ]
}
```

## Return ReactedResult Type Object

```json
{
  "success": true,
  "message": "",
  "model": {
    "id": "0194090d-a1e5-7f27-b534-1ee26916b532",
    "name": "name1",
    "surname": "surname1",
    "firmId": "0194090d-a254-7c7d-9b6d-4a036bc65a73",
    "firm": {
      "id": "0194090d-a254-7c7d-9b6d-4a036bc65a73",
      "name": "firm1",
      "locationId": "0194090d-a26a-7b8e-af07-135bc015af71",
      "location": {
        "id": "0194090d-a26a-7b8e-af07-135bc015af71",
        "name": "firm1street",
        "nviAddress": "123456",
        "address": "firm1address",
        "locationType": "Work"
      },
      "contacts": [
        null
      ]
    },
    "contactInformations": [
      {
        "id": "0194090d-a236-7e15-9d8e-96829f3fd5f5",
        "contactId": "0194090d-a1e5-7f27-b534-1ee26916b532",
        "contactType": "Phone",
        "info": "+905559993636",
        "contact": null
      },
      {
        "id": "0194090d-a251-7a76-ba43-fc28cf2065de",
        "contactId": "0194090d-a1e5-7f27-b534-1ee26916b532",
        "contactType": "Email",
        "info": "contac1@contacts.com",
        "contact": null
      }
    ]
  },
  "statusCode": "OK"
}
```

## Database State

```json
{
"Contact": [
	{
		"Id" : "0194090d-a1e5-7f27-b534-1ee26916b532",
		"Name" : "name1",
		"Surname" : "surname1",
		"FirmId" : "0194090d-a254-7c7d-9b6d-4a036bc65a73"
	}
]
}
```
```json
{
    "ContactInfo": [
    	{
    		"Id" : "0194090d-a236-7e15-9d8e-96829f3fd5f5",
    		"ContactId" : "0194090d-a1e5-7f27-b534-1ee26916b532",
    		"ContactType" : 0,
    		"Info" : "+905559993636"
    	},
    	{
    		"Id" : "0194090d-a251-7a76-ba43-fc28cf2065de",
    		"ContactId" : "0194090d-a1e5-7f27-b534-1ee26916b532",
    		"ContactType" : 1,
    		"Info" : "contac1@contacts.com"
    	}
    ]
}
```
```json
{
    "Firm": [
    	{
    		"Id" : "0194090d-a254-7c7d-9b6d-4a036bc65a73",
    		"Name" : "firm1",
    		"LocationId" : "0194090d-a26a-7b8e-af07-135bc015af71"
    	}
    ]
}
```
```json
{
    "Location": [
    	{
    		"Id" : "0194090d-a26a-7b8e-af07-135bc015af71",
    		"Name" : "firm1street",
    		"Address" : "firm1address",
    		"NVIAddress" : "123456",
    		"LocationType" : 0
    	}
    ]
}
```

## Kafka

After contact is added a kafka message from ContactService (in pigeon-crud-service) to ReportService (in pigeon-report-service). ReportService updates info such as below.

```json
{
"Info": [
	{
		"Id" : "01940ce6-4247-7b84-8561-0985758aad97",
		"Details" : "\"Count\"=>\"1\"",
		"InfoType" : 0
	}
]}

```
