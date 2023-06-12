using Newtonsoft.Json;
using System.Net.Http.Json;
using Woningpaspoort.Model;
using ZXing;
using static System.Net.Mime.MediaTypeNames;
using Image = Woningpaspoort.Model.Image;

namespace Woningpaspoort.Data;

public class ApiManager
{
    static readonly string BaseAddress = "https://e47b-37-114-89-117.ngrok-free.app";
    //static readonly string BaseAddress = "http://192.168.1.240:5045";

    static readonly string Url = $"{BaseAddress}/api/";

    static HttpClient client;

    private static async Task<HttpClient> GetClient()
    {
        if (client != null)
            return client;

        client = new HttpClient();

        return client;
    }

    //Onderstaande alle requests mbt woningen
    public static async Task<IEnumerable<House>> GetAllHouses()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return new List<House>();

        HttpClient client = await GetClient();
        string result = await client.GetStringAsync($"{Url}House");

        return JsonConvert.DeserializeObject<List<House>>(result);
    }

    public static async Task<House> GetHouseByCode(string code)
    {
        HttpClient client = await GetClient();
        string result = await client.GetStringAsync($"{Url}House/{code}");

        return JsonConvert.DeserializeObject<House>(result);
    }

    public static async Task<House> PostHouse(string street, int number, string addition, string zipcode, string city, string country, string externalcode, string customer, int yearBuild, bool daeb, string createdBy)
    {
        HouseToAdd house = new HouseToAdd()
        {
            Street = street,
            Number = number,
            Addition = addition,
            ZipCode = zipcode,
            City = city,
            Country = country,
            ExternalCode = externalcode,
            Customer = customer,
            YearBuild = yearBuild,
            Daeb = daeb,
            CreatedBy = createdBy
        };

        var msg = new HttpRequestMessage(HttpMethod.Post, $"{Url}House");
        msg.Content = JsonContent.Create<HouseToAdd>(house);

        var response = await client.SendAsync(msg);

        response.EnsureSuccessStatusCode();

        var returnedJson = await response.Content.ReadAsStringAsync();
        var insertedHouse = JsonConvert.DeserializeObject<House>(returnedJson);

        return insertedHouse;
    }

    //Onderstaande alle requests mbt images
    public static async Task<Image> PostImage(string description, bool thumbnail, string createdBy, FileResult file)
    {
        await using var stream = System.IO.File.OpenRead(file.FullPath);

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{Url}Image");

        using var content = new MultipartFormDataContent
        {
            { new StreamContent(stream), "file", "file.jpg" },

            { new StringContent(description), "Description" },
            { new StringContent(createdBy), "CreatedBy" },
            { new StringContent("true"), "Thumbnail" }
        };

        request.Content = content;

        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var returnedJson = await response.Content.ReadAsStringAsync();
        var insertedImage = JsonConvert.DeserializeObject<Image>(returnedJson);

        return insertedImage;
    }

    public static async void PostHouseImage(int houseId, int imageId)
    {
        HouseImageToAdd houseImage = new HouseImageToAdd()
        {
            houseObjectId = houseId,
            imageId = imageId
        };

        var msg = new HttpRequestMessage(HttpMethod.Post, $"{Url}HouseImage");
        msg.Content = JsonContent.Create<HouseImageToAdd>(houseImage);

        var response = await client.SendAsync(msg);

        response.EnsureSuccessStatusCode();
    }

    //Onderstaande alle requests mbt documents
    public static async Task<Document> PostDocument(string description, string createdBy, FileResult file)
    {
        await using var stream = System.IO.File.OpenRead(file.FullPath);

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{Url}Document");

        using var content = new MultipartFormDataContent
        {
            { new StreamContent(stream), "file", "file.pdf" },

            { new StringContent(description), "Description" },
            { new StringContent(createdBy), "CreatedBy" }
        };

        request.Content = content;

        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var returnedJson = await response.Content.ReadAsStringAsync();
        var insertedDocument = JsonConvert.DeserializeObject<Document>(returnedJson);

        return insertedDocument;
    }

    public static async void PostHouseDocument(int houseId, int documentId)
    {
        HouseDocumentToAdd houseDocument = new HouseDocumentToAdd()
        {
            houseObjectId = houseId,
            documentId = documentId
        };

        var msg = new HttpRequestMessage(HttpMethod.Post, $"{Url}HouseDocument");
        msg.Content = JsonContent.Create<HouseDocumentToAdd>(houseDocument);

        var response = await client.SendAsync(msg);

        response.EnsureSuccessStatusCode();
    }

    public static async Task<Stream> GetImage(string externalURL)
    {
        HttpClient client = await GetClient();
        var result = await client.GetAsync(externalURL);
        var stream = new MemoryStream(await result.Content.ReadAsByteArrayAsync());

        return stream;
    }
}