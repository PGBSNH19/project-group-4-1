using API.Configuration;
using API.Context;
using API.Controllers;
using API.Dtos;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.ControllerTests
{
    public class MarketplaceControllerTests
    {
        private readonly IMapper _mapper;
        public MarketplaceControllerTests()
        {
            var mappedProfile = new MappedProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(mappedProfile));
            IMapper mapper = new Mapper(configuration);
            _mapper = mapper;
        }

        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());
            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);
            var marketplaceController = new MarketplaceController(marketplaceRepository, _mapper);

            //Act
            var result = await marketplaceController.GetMarketplaces();
            var contentResult = result.Result as OkObjectResult;
            var resultMarketplaces = contentResult.Value as MarketplaceDto[];

            //Assert
            Assert.True(resultMarketplaces.Length > 0);
        }

        [Fact]
        public async void GetById_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());
            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);
            var marketplaceController = new MarketplaceController(marketplaceRepository, _mapper);

            //Act
            var result = await marketplaceController.GetMarketplaceById(1);
            var contentResult = result.Result as OkObjectResult;
            var resultMarketplace = contentResult.Value as MarketplaceDto;

            //Assert
            Assert.NotNull(resultMarketplace);
        }

        [Fact]
        public async void GetById_IfNotExist_ExpectedNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());
            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);
            var marketplaceController = new MarketplaceController(marketplaceRepository, _mapper);

            //Act
            var result = await marketplaceController.GetMarketplaceById(1);
            var contentResult = result.Result as NotFoundObjectResult;

            //Assert
            Assert.Null(result.Value);
        }

        [Fact]
        public async void PostMarketplace_IfMarketplaceIsPosted_Expected201StatusCode()
        {
            //Arrange
            var marketplaceRepository = new Mock<IMarketplaceRepository>();
            marketplaceRepository.Setup(x => x.Save()).Returns(Task.FromResult(true));
            var marketplaceController = new MarketplaceController(marketplaceRepository.Object, _mapper);

            //Act
            var createdResult = await marketplaceController.PostMarketplace(new MarketplaceDto
            {
                MarketplaceID = 3,
                Name = "Market3",
                Location = "Malmö",
                StartDateTime = new DateTime(2020, 12, 22),
                EndDateTime = new DateTime(2020, 12, 23)
            });
            var contentResult = createdResult.Result as CreatedResult;

            //Assert
            Assert.Equal(201, contentResult.StatusCode);
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
