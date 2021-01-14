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
            var dataResult = await marketplaceController.GetMarketplaces();
            var contentResult = dataResult.Result as OkObjectResult;
            var resultMarketplaces = contentResult.Value as List<MarketplaceDto>;
            //Assert
            Assert.True(resultMarketplaces.Count > 0);
            Assert.IsType<OkObjectResult>(contentResult);
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
            Assert.IsType<OkObjectResult>(contentResult);
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
            var contentResult = result.Result as NoContentResult;

            //Assert
            Assert.Null(result.Value);
            Assert.IsType<NoContentResult>(contentResult);
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

        [Fact]
        public async void PutMarketplace_IfMarketplaceIsUpdated_ExpectedStatusCode200()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());
            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);

            var controller = new MarketplaceController(marketplaceRepository, _mapper);
            var newMarket = new MarketplaceDto
            {
                MarketplaceID = 7,
                Name = "Market1",
                Location = "Göteborg",
                StartDateTime = new DateTime(2020, 12, 18),
                EndDateTime = new DateTime(2020, 12, 19),
                Image = "",
            };
            var result = await controller.PutMarketplace(1, newMarket);
            var content = result.Result as OkObjectResult;
            Assert.Equal(200, content.StatusCode);
        }

        [Fact]
        public async void PutMarketplace_IfMarketplaceIsNotUpdated_ExpectedStatusCode400()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());
            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);

            var controller = new MarketplaceController(marketplaceRepository, _mapper);
            var newMarket = new MarketplaceDto
            {
                MarketplaceID = 7,
                Name = "Market1",
                Location = "Göteborg",
                StartDateTime = new DateTime(2020, 12, 18),
                EndDateTime = new DateTime(2020, 12, 19),
                Image = "",
            };
            var result = await controller.PutMarketplace(55, newMarket);
            var content = result.Result as BadRequestObjectResult;
            Assert.Equal(400, content.StatusCode);
        }

        [Fact]
        public async void DeleteMarketplace_IfMarketplaceIsDeleted_ExpectedStatusCode204()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());
            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);

            var controller = new MarketplaceController(marketplaceRepository, _mapper);
            var result = await controller.DeleteMarketplace(1);
            var content = result as NoContentResult;
            Assert.Equal(204, content.StatusCode);
        }

        [Fact]
        public async void DeleteMarketplace_IfMarketplaceIsNull_ExpectedStatusCode400()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Marketplaces).ReturnsDbSet(GetMarketplaces());
            var marketplaceRepository = new MarketplaceRepository(mockContext.Object);

            var controller = new MarketplaceController(marketplaceRepository, _mapper);
            var result = await controller.DeleteMarketplace(66);
            var content = result as NotFoundResult;
            Assert.Equal(404, content.StatusCode);
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
                    EndDateTime = new DateTime(2020, 12, 19),
                    PictureBytes = null
                },
                new Marketplace()
                {
                    MarketplaceID = 2,
                    Name = "Market2",
                    Location = "Stockholm",
                    StartDateTime = new DateTime(2020, 12, 20),
                    EndDateTime = new DateTime(2020, 12, 21),
                    PictureBytes = null

                }
            };
        }
    }
}
