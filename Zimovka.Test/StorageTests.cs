using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Zimovka.Model;


namespace Zimovka.Tests
{
    public class StorageTests
    {
        private readonly Storage _storage;

        public StorageTests()
        {
            _storage = new Storage();
        }

        [Fact]
        public void Search_ReturnsEmptyList_WhenNoDataMatchesCriteria()
        {
            // Arrange
            DateTime startDate = new DateTime(2023, 1, 1);
            DateTime endDate = new DateTime(2023, 1, 31);
            string region = "NonExistentRegion";

            // Act
            var result = _storage.Search(startDate, endDate, region);

            // Assert
            Assert.Empty(result);
        }


        [Fact]
        public void Search_ReturnsDataWithinDateRange()
        {
            // Arrange
            string testData = "2023-01-15,RegionA,10,100\n2023-01-20,RegionA,15,150\n2023-02-01,RegionA,20,200";
            File.WriteAllText("./TestData.csv", testData); // Create test file

            DateTime startDate = new DateTime(2023, 1, 15);
            DateTime endDate = new DateTime(2023, 1, 20);
            string region = "RegionA";

            // Act
            var result = _storage.Search(startDate, endDate, region);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, item => Assert.True(item._date >= startDate && item._date <= endDate));
        }

        // Clean up test data file after tests run
        public void Dispose()
        {
            if (File.Exists("./TestData.csv"))
            {
                File.Delete("./TestData.csv");
            }
        }
    }
}