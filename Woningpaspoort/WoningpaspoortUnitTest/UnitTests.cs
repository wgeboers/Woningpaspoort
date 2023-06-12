using NSubstitute;
using Woningpaspoort.Model;
using Woningpaspoort.Services;
using Woningpaspoort.ViewModel;

namespace WoningpaspoortUnitTest;

public class WoningpaspoortUnitTest
{
    [Fact]
    public async Task ShouldPopulateHouses()
    {
        //Arrange
        var houses = new List<House> { 
            new House
            {
                Code = "3826BN46",
                Street = "Blokzijlpark",
                Number = 46,
                ZipCode = "3826BN",
                City = "Amersfoort",
                Country = "Nederland",
                Customer = "De alliantie",
                CreatedBy = "WGeboers"
            },
            new House
            {
                Code = "3813JD34",
                Street = "Olivierplaats",
                Number = 34,
                ZipCode = "3813JD",
                City = "Amersfoort",
                Country = "Nederland",
                Customer = "Portaal",
                CreatedBy = "WGeboers"
            }
        };

        var houseService = Substitute.For<HouseService>();
        houseService.GetHouses().Returns(houses);

        //Act
        var housesVM = new HousesViewModel(houseService);

        //Assert
        Assert.True(housesVM.Houses?.Count > 0);
    }

    [Fact]
    public async Task ShouldRetrieveCustomers()
    {
        //Arrange
        var houses = new List<House> {
            new House
            {
                Code = "3826BN46",
                Street = "Blokzijlpark",
                Number = 46,
                ZipCode = "3826BN",
                City = "Amersfoort",
                Country = "Nederland",
                Customer = "De alliantie",
                CreatedBy = "WGeboers"
            },
            new House
            {
                Code = "3813JD34",
                Street = "Olivierplaats",
                Number = 34,
                ZipCode = "3813JD",
                City = "Amersfoort",
                Country = "Nederland",
                Customer = "Portaal",
                CreatedBy = "WGeboers"
            }
        };

        var houseService = Substitute.For<HouseService>();
        houseService.GetHouses().Returns(houses);

        //Act
        var housesVM = new HousesViewModel(houseService);
        var result = housesVM.GetCustomers();

        //Assert
        Assert.True(result.Count > 0);
    }

    [Fact]
    public async Task ShouldPopulateHouseWithHouse3826BN46()
    {
        //Arrange
        var house = new House
        {
            Code = "3826BN46",
            Street = "Blokzijlpark",
            Number = 46,
            ZipCode = "3826BN",
            City = "Amersfoort",
            Country = "Nederland",
            Customer = "De alliantie",
            CreatedBy = "WGeboers"
        };

        var code = "3826BN46";
        var houseService = Substitute.For<HouseService>();
        houseService.GetHouseByCode(code).Returns(house);

        //Act
        var housesVM = new HousesViewModel(houseService);
        var result = housesVM.GetHouseByCode(code);
        var returnedHouse = housesVM.house;

        //Assert
        Assert.Equal(code, returnedHouse.Code);
    }
}