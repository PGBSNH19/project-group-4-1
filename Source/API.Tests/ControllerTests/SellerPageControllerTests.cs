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
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace API.Tests.ControllerTests
{
    public class SellerPageControllerTests
    {
        private readonly IMapper _mapper;
        public SellerPageControllerTests()
        {
            var mappedProfile = new MappedProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(mappedProfile));
            IMapper mapper = new Mapper(configuration);
            _mapper = mapper;
        }

        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //arange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.SellerPages).ReturnsDbSet(GetSellerPages());
            var sellerPageRepository = new SellerPageRepository(mockContext.Object);
            var productPageRepository = new ProductRepository(mockContext.Object);
            var sellerpageController = new SellerPageController(sellerPageRepository, _mapper, productPageRepository);

            var result = await sellerpageController.GetSellerPages();
            var contentResult = result.Result as OkObjectResult;
            var resultSellerPages = contentResult.Value as SellerPageDto[];

            Assert.True(resultSellerPages.Length > 0);
        }

        [Fact]

        public async void GetSellerPageById_IfExist_ReturnTrue()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.SellerPages).ReturnsDbSet(GetSellerPages());
            var sellerPagesRepository = new SellerPageRepository(mockContext.Object);
            var productPageRepository = new ProductRepository(mockContext.Object);
            var sellPageController = new SellerPageController(sellerPagesRepository, _mapper, productPageRepository);

            var result = await sellPageController.GetSellerPageByUserId(1);
            var contentResult = result.Result as OkObjectResult;
            var resultSellerPage = contentResult.Value as SellerPageDto;

            Assert.NotNull(resultSellerPage);

        }
        [Fact]

        public async void GetSellerPageById_IfNotExist_ExpectedNull()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.SellerPages).ReturnsDbSet(GetSellerPages());
            var sellerPagesRepository = new SellerPageRepository(mockContext.Object);
            var productPagesRepository = new ProductRepository(mockContext.Object);
            var sellPageController = new SellerPageController(sellerPagesRepository, _mapper, productPagesRepository);

            var result = await sellPageController.GetSellerPageByUserId(4);
            var contentResult = result.Result as NotFoundObjectResult;

            Assert.Null(result.Value);
        }

        [Fact]
        public async void PostSellerPage_IfPostSellerPages_Expected201StatusCode()
        {
            var sellerPagesRepository = new Mock<ISellerPageRepository>();
            var productRepository = new Mock<IProductRepository>();
            sellerPagesRepository.Setup(x => x.Save()).Returns(Task.FromResult(true));
            productRepository.Setup(x => x.Save()).Returns(Task.FromResult(true));
            var sellerController = new SellerPageController(sellerPagesRepository.Object, _mapper, productRepository.Object);

            var createdResult = await sellerController.PostSellerPage(new SellerPageDto
            {
                SellerPageID = 3,
                Name = "Sebastian",
                SellerUserID = 3
            });
            var contentResult = createdResult.Result as CreatedResult;

            Assert.Equal(201, contentResult.StatusCode);
        }
        public List<SellerPage> GetSellerPages()
        {
            return new List<SellerPage>
            {
                new SellerPage
                {
                    SellerPageID = 1,
                    Name = "Anders",
                    SellerUserID=1

                } ,
                new SellerPage
                {
                    SellerPageID = 2,
                    Name = "Oskar",
                    SellerUserID=2

                }

            };
        }
    }
}
