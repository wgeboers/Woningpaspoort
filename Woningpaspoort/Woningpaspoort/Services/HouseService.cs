using Woningpaspoort.Data;
using Woningpaspoort.Model;

namespace Woningpaspoort.Services;

public class HouseService
{
	List<House> houseList = new();
    House house;
    House postedHouse;

	public virtual async Task<List<House>> GetHouses()
	{
        houseList = (List<House>)await ApiManager.GetAllHouses();

        return houseList;
    }

    public virtual async Task<House> GetHouseByCode(string code)
    {
        House resultHouses = await ApiManager.GetHouseByCode(code);
        house = resultHouses;

        return house;
    }

    public async Task<House> PostHouse(string street, int number, string addition, string zipcode, string city, string country, string externalcode, string customer, int yearBuild, bool daeb, string createdBy)
    {
        House responsehouse = await ApiManager.PostHouse(street, number, addition, zipcode, city, country, externalcode, customer, yearBuild, daeb, createdBy);
        postedHouse = responsehouse;

        return postedHouse;
    }

    public async Task<Stream> GetImage(string externalURL)
    {
        Stream image = await ApiManager.GetImage(externalURL);

        return image;
    }
}