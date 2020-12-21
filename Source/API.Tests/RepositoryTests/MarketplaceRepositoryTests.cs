using System;
using System.Collections.Generic;
using API.Context;
using API.Models;
using API.Services;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace API.Tests.RepositoryTests
{
    public class MarketplaceRepositoryTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());
            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);

            //Act
            var result = await marketplaceRepository.GetMarketplaces();

            //Assert
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async void GetById_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());
            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);

            //Act
            var result = await marketplaceRepository.GetMarketplaceById(1);

            //Assert
            Assert.NotNull(result);
        }

        public List<Marketplace> GetMarketplaces()
        {
            return new List<Marketplace>
            {
                new Marketplace()
                {
                    MarketplaceID = 1,
                    Name = "Market1",
                    Location = "Göteborg",
                    StartDateTime = new DateTime(2020, 12, 18),
                    EndDateTime = new DateTime(2020, 12, 19)
                },
                new Marketplace()
                {
                    MarketplaceID = 2,
                    Name = "Market2",
                    Location = "Stockholm",
                    StartDateTime = new DateTime(2020, 12, 20),
                    EndDateTime = new DateTime(2020, 12, 21)
                }
            };
        }
    }
}
