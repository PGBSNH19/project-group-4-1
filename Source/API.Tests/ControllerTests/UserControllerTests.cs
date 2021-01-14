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
    public class UserControllerTests
    {
        private readonly IMapper _mapper;
        public UserControllerTests()
        {
            var mappedProfile = new MappedProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(mappedProfile));
            IMapper mapper = new Mapper(configuration);
            _mapper = mapper;
        }

        #region GetUsers tests
        [Fact]
        public async void GetUsers_IfAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var result = await userController.GetUsers();
            var contentResult = result.Result as ObjectResult;
            var resultUsers = contentResult.Value as UserDto[];

            //Assert
            Assert.True(resultUsers.Length > 0);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void GetUsers_IfNoResults_ReturnNoContent()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var result = await userController.GetUsers();

            //Assert
            Assert.IsType<NoContentResult>(result.Result);
        }
        #endregion

        #region GetUser tests
        [Fact]
        public async void GetUser_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var result = await userController.GetUser(1);
            var contentResult = result.Result as OkObjectResult;
            var resultUsers = contentResult.Value as UserDto;

            //Assert
            Assert.NotNull(resultUsers);
        }

        [Fact]
        public async void GetUser_IfNotExist_ExpectedNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var result = await userController.GetUser(4);

            //Assert
            Assert.Null(result.Value);
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void GetUser_BadRequestData_ReturnBadRequest(object id)
        {
            //Arrange
            var userController = new UserController(null, _mapper);

            //Act
            var result = await userController.GetUser((int)id);

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }
        #endregion

        #region GetUserByName tests
        [Fact]
        public async void GetUserByName_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var result = await userController.GetUserByName("Example1");
            var contentResult = result.Result as OkObjectResult;
            var resultUsers = contentResult.Value as UserDto;

            //Assert
            Assert.NotNull(resultUsers);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void GetUserByName_IfNotExist_ExpectedNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var result = await userController.GetUserByName("Klas");

            //Assert
            Assert.Null(result.Value);
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void GetUserByName_BadRequestData_ReturnNoContent(string name)
        {
            //Arrange
            var userController = new UserController(null, _mapper);

            //Act
            var result = await userController.GetUserByName(name);

            //Assert
            Assert.Null(result.Value);
            Assert.IsType<BadRequestResult>(result.Result);
        }
        #endregion

        #region GetUserByEmail tests
        [Fact]
        public async void GetUserByEmail_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var result = await userController.GetUserByEmail("example1@examplesson.se");
            var contentResult = result.Result as OkObjectResult;
            var resultUsers = contentResult.Value as UserDto;

            //Assert
            Assert.NotNull(resultUsers);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void GetUserByEmail_IfNotExist_ExpectedNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var result = await userController.GetUserByEmail("Hejsan");
            var contentResult = result.Result as NotFoundObjectResult;

            //Assert
            Assert.Null(result.Value);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void GetUserByEmail_BadRequestData_ReturnNoContent(string email)
        {
            //Arrange
            var userController = new UserController(null, _mapper);

            //Act
            var result = await userController.GetUserByEmail(email);

            //Assert
            Assert.Null(result.Value);
            Assert.IsType<BadRequestResult>(result.Result);
        }
        #endregion

        #region PostUser tests
        [Fact]
        public async void PostUser_IfUserPosted_Expected201StatusCode()
        {
            //Arrange
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.Save()).Returns(Task.FromResult(true));
            var userController = new UserController(userRepository.Object, _mapper);

            //Act
            var createdResult = await userController.PostUser(new UserDto
            {
                UserID = 3,
                Email = "example3@examplesson.se",
                Username = "Example3",
                Password = "12345"
            });
            var contentResult = createdResult.Result as CreatedResult;

            //Assert
            Assert.Equal(201, contentResult.StatusCode);
        }
        #endregion

        #region DeleteUser tests 
        [Fact]
        public async void DeleteUser_BadRequestData_ReturnBadRequest()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var badId = 0;
            var result = await userController.DeleteUser(badId);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void DeleteUser_IfNotExist_ReturnNoContent()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var nonExistingId = 5;
            var result = await userController.DeleteUser(nonExistingId);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        #endregion

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
