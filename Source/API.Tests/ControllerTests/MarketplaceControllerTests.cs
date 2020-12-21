using API.Context;
using API.Controllers;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace API.Tests.ControllerTests
{
    public class MarketplaceControllerTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());

            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);
            var marketplaceController = new MarketplaceController(marketplaceRepository);

            var result = await marketplaceController.GetMarketplaces();
            var contentResult = result.Result as OkObjectResult;
            var resultMarketplaces = contentResult.Value as Marketplace[];

            Assert.True(resultMarketplaces.Length > 0);
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
