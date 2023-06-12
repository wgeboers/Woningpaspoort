namespace Woningpaspoort.Model;

public class House
{
    public int ObjectId { get; set; }
    public required string Code { get; set; }
    public required string Street { get; set; }
    public int Number { get; set; }
    public string? Addition { get; set; }
    public required string ZipCode { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public string? ExternalCode { get; set; }
    public required string Customer { get; set; }
    public int? YearBuild { get; set; }
    public bool Daeb { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public required string? CreatedBy { get; set; }

    public ICollection<Complex>? Complexes { get; set; }
    public ICollection<ServiceContract>? ServiceContracts { get; set; }
    public ICollection<Document>? Documents { get; set; }
    public ICollection<Image>? Images { get; set; }
    public ICollection<Serviceorder>? serviceorders { get; set; }

    public Image Thumbnail
    {
        get { return this.Images.Where(b => b.Thumbnail == true).FirstOrDefault(); }
    }
}