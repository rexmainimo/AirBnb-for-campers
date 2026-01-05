using AirBnb_for_campers.Data;
using AirBnb_for_campers.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AirBnb_for_campers.Tests
{
    public class CampingSpotDataTests
    {
        [Fact]
        public async Task GetCampingSpotsByCity_ReturnsFilteredSpots_WhenCityMatches()
        {
            // Arrange
            var mockSpots = new List<CampingSpot>
        {
            new CampingSpot { Id = 1, City = "Brussels" },
            new CampingSpot { Id = 2, City = "Antwerp" },
            new CampingSpot { Id = 3, City = "Brussels" }
        };

            var mockService = new Mock<ICampingSpot>();
            mockService
                .Setup(s => s.GetSpotByCity("Brussels"))
                .Returns(mockSpots.Where(s => s.City == "Brussels"));


            // Act
            var result = mockService.Object.GetSpotByCity("Brussels");

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, s => Assert.Equal("Brussels", s.City));
        }
    }

}
