namespace Woningpaspoort.Model;

public class HouseToAdd
{
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
    public required string? CreatedBy { get; set; }
}