﻿using API.Context;
using API.Controllers;
using API.Models;
using API.Services;
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
        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //arange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.SellerPages).ReturnsDbSet(GetSellerPages());
            var sellerPageRepository = new SellerPageRepository(mockContext.Object);
            var sellerpageController = new SellerPageController(sellerPageRepository);

            var result = await sellerpageController.GetSellerPages();
            var contentResult = result.Result as OkObjectResult;
            var resultSellerPages = contentResult.Value as SellerPage[];

            Assert.True(resultSellerPages.Length > 0);
        }

        [Fact]

        public async void GetSellerPageById_IfExist_ReturnTrue()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.SellerPages).ReturnsDbSet(GetSellerPages());
            var sellerPagesRepository = new SellerPageRepository(mockContext.Object);
            var sellPageController = new SellerPageController(sellerPagesRepository);

            var result = await sellPageController.GetSellerPageByUserId(1);
            var contentResult = result.Result as OkObjectResult;
            var resultSellerPage = contentResult.Value as SellerPage;

            Assert.NotNull(resultSellerPage);

        }
        [Fact]
        public async void PostSellerPage_IfPostSellerPages_Expected201StatusCode()
        {
            var sellerPagesRepository = new Mock<ISellerPageRepository>();
            sellerPagesRepository.Setup(x => x.Save()).Returns(Task.FromResult(true));

            var sellerController = new SellerPageController(sellerPagesRepository.Object);

            var createdResult = await sellerController.PostSellerPage(new SellerPage
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
