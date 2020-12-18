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
    public class UserControllerTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());

            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository);

            var result = await userController.GetUsers();
            var contentResult = result.Result as OkObjectResult;
            var resultUsers = contentResult.Value as User[];

            Assert.True(resultUsers.Length > 0);
        }

        [Fact]
        public async void GetById_IfExist_ExpectedNotNull()
        {
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());

            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository);

            var result = await userController.GetUser(1);
            var contentResult = result.Result as OkObjectResult;
            var resultUsers = contentResult.Value as User;

            Assert.NotNull(resultUsers);
        }

        [Fact]
        public async void PostUser_IfUserPosted_Expected201StatusCode()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.Save()).Returns(Task.FromResult(true));
            var userController = new UserController(userRepository.Object);

            var createdResult = await userController.PostUser(new User()
            {
                UserID = 3,
                Email = "example3@examplesson.se",
                Username = "Example3",
                Password = "12345"
            });
            var contentResult = createdResult.Result as CreatedResult;

            Assert.Equal(201, contentResult.StatusCode);
        }

        public List<User> GetUsers()
        {
            return new List<User>
            {
                new User()
                {
                    UserID = 1,
                    Email = "example1@examplesson.se",
                    Username = "Example1",
                    Password = "123"
                },
                new User()
                {
                    UserID = 2,
                    Email = "example2@examplesson.se",
                    Username = "Example2",
                    Password = "1234"
                }
            };
        }
    }
}
