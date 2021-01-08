using API.Context;
using API.Models;
using API.Services;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace API.Tests.RepositoryTests
{
    public class SellerPageRepositoryTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.SellerPages).ReturnsDbSet(GetSellerPages());
            var sellerPageRepository = new SellerPageRepository(mockContext.Object);

            //Act
            var result = await sellerPageRepository.GetSellerPages();

            //Assert
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async void GetById_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.SellerPages).ReturnsDbSet(GetSellerPages());
            var sellerPageRepository = new SellerPageRepository(mockContext.Object);

            //Act
            var result = await sellerPageRepository.GetSellerPageByUserID(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetById_IfNotExist_ExpectedNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.SellerPages).ReturnsDbSet(GetSellerPages());
            var sellerPageRepository = new SellerPageRepository(mockContext.Object);

            //Act
            var result = await sellerPageRepository.GetSellerPageByUserID(3);

            //Assert
            Assert.Null(result);
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
