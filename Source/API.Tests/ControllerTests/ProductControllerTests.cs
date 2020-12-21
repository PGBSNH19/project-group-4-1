using System.Collections.Generic;
using System.Threading.Tasks;
using API.Context;
using API.Controllers;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace API.Tests.ControllerTests
{
    public class ProductControllerTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(GetProducts());
            var productRepository = new ProductRepository(mockContext.Object);
            var productController = new ProductController(productRepository);

            //Act
            var result = await productController.GetProducts();
            var contentResult = result.Result as OkObjectResult;
            var resultProducts = contentResult.Value as Product[];

            //Assert
            Assert.True(resultProducts.Length > 0);
        }

        [Fact]
        public async void GetById_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(GetProducts());
            var productRepository = new ProductRepository(mockContext.Object);
            var productController = new ProductController(productRepository);

            //Act
            var result = await productController.GetProductById(1);
            var contentResult = result.Result as OkObjectResult;
            var resultProduct = contentResult.Value as Product;

            //Assert
            Assert.NotNull(resultProduct);
        }

        [Fact]
        public async void PostProduct_IfProductPosted_Expected201StatusCode()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.Save()).Returns(Task.FromResult(true));
            var productController = new ProductController(productRepository.Object);

            //Act
            var createdResult = await productController.PostProduct(new Product()
            {
                ProductID = 3,
                Name = "Product3"
            });
            var contentResult = createdResult.Result as CreatedResult;

            //Assert
            Assert.Equal(201, contentResult.StatusCode);
        }

        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product()
                {
                    ProductID = 1,
                    Name = "Product1"
                },
                new Product()
                {
                    ProductID = 2,
                    Name = "Product2"
                }
            };
        }
    }
}
