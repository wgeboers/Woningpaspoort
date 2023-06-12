using Woningpaspoort.Data;
using Woningpaspoort.Model;
using Image = Woningpaspoort.Model.Image;

namespace Woningpaspoort.Services;

public class ImageService
{
	Image postedImage;
	public async Task<Image> PostImage(string description, bool thumbnail, string createdBy, FileResult file)
	{
		Image responseimage = await ApiManager.PostImage(description, thumbnail, createdBy, file);
        postedImage = responseimage;

		return responseimage;
	}

	public async Task PostHouseImage(int houseId, int imageId)
	{
		ApiManager.PostHouseImage(houseId, imageId);
	}
}