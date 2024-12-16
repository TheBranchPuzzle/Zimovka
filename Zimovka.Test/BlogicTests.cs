using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Zimovka.Model;
using Zimovka.Data;
using Zimovka.Presenter;


namespace Zimovka.Test
{
    public class BlogicTests
    {

    private readonly Mock<IStorage> _mockStorage;
    private readonly BLogic _bLogic;

    public BlogicTests()
    {
        _mockStorage = new Mock<IStorage>();
        _bLogic = new BLogic(_mockStorage.Object);
    }

    [Fact]
    public void Search_ShouldReturnCorrectRegionOutputs()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-7);
        var endDate = DateTime.Now;
        var regions = new List<string> { "Region1", "Region2" };

    var region1Data = new List<RegionItem>
        {
            new RegionItem { _date = DateTime.Now.AddDays(-1), _regionName = "Region1", _temp = 20, _price = 100 },
            new RegionItem { _date = DateTime.Now.AddDays(-2), _regionName = "Region1", _temp = 22, _price = 110 }
        };

        var region2Data = new List<RegionItem>
        {
            new RegionItem { _date = DateTime.Now.AddDays(-1), _regionName = "Region2", _temp = 18, _price = 90 },
            new RegionItem { _date = DateTime.Now.AddDays(-2), _regionName = "Region2", _temp = 19, _price = 95 }
        };

        _mockStorage.Setup(s => s.Search(startDate, endDate, "Region1")).Returns(region1Data);
        _mockStorage.Setup(s => s.Search(startDate, endDate, "Region2")).Returns(region2Data);

        // Act
        var result = _bLogic.Search(startDate, endDate, regions);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Region1", result[0].Region);
        Assert.Equal(21, result[0].avgTemp); // Average of 20 and 22
        Assert.Equal(105, result[0].avgPrice); // Average of 100 and 110
        
        Assert.Equal("Region2", result[1].Region);
        Assert.Equal(18, result[1].avgTemp); // Average of 18 and 19
        Assert.Equal(92, result[1].avgPrice); // Average of 90 and 95
    }

    [Fact]
        public void AddFav_ShouldAddToFavorites()
        {
            // Arrange
            _bLogic.CurrentSearch = new List<RegionOutput>
            {
                new RegionOutput { Region = "Region1", avgTemp = 21, avgPrice = 105 },
                new RegionOutput { Region = "Region2", avgTemp = 18, avgPrice = 92 }
            };

            // Act
            _bLogic.AddFav(1);

            // Assert
            Assert.Single(_bLogic.Favorites);
            Assert.Equal("Region1", _bLogic.Favorites[0].Region);
        }

        [Fact]
        public void RemoveFav_ShouldRemoveFromFavorites()
        {
            // Arrange
            _bLogic.Favorites = new List<RegionOutput>
            {
                new RegionOutput { Region = "Region1", avgTemp = 21, avgPrice = 105 },
                new RegionOutput { Region = "Region2", avgTemp = 18, avgPrice = 92 }
            };

            // Act
            _bLogic.RemoveFav(1);

            // Assert
            Assert.Single(_bLogic.Favorites);
            Assert.Equal("Region2", _bLogic.Favorites[0].Region);
        }

        [Fact]
        public void Analyse_ShouldReturnTopTwoRegionsByPrice()
        {
            // Arrange
            _bLogic.CurrentSearch = new List<RegionOutput>
            {
                new RegionOutput { Region = "Region1", avgTemp = 21, avgPrice = 105 },
                new RegionOutput { Region = "Region2", avgTemp = 18, avgPrice = 92 },
                new RegionOutput { Region = "Region3", avgTemp = 25, avgPrice = 80 }
            };

            // Act
            var result = _bLogic.Analyse();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Region3", result[0]); // Lowest price
            Assert.Equal("Region2", result[1]); // Second lowest price
        }
    }
}
