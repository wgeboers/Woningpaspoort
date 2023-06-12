using Woningpaspoort.Data;
using Woningpaspoort.Model;

namespace Woningpaspoort.Services;

public class DocumentService
{
	Document postedDocument;
	public async Task<Document> PostDocument(string description, string createdBy, FileResult file)
	{
		Document responseDocument = await ApiManager.PostDocument(description, createdBy, file);
		postedDocument = responseDocument;

		return responseDocument;
	}

	public async Task PostHouseDocument(int houseId, int documentId)
	{
		ApiManager.PostHouseDocument(houseId, documentId);
	}
}