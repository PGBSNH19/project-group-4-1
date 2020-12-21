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
    public class ProductRepositoryTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(GetProducts());
            var productRepository = new ProductRepository(mockContext.Object);

            //Act
            var result = await productRepository.GetProducts();

            //Assert
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async void GetById_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(GetProducts());
            var productRepository = new ProductRepository(mockContext.Object);

            //Act
            var result = await productRepository.GetProductById(1);

            //Assert
            Assert.NotNull(result);
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
