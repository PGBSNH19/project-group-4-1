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

        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);
            var userController = new UserController(userRepository, _mapper);

            //Act
            var result = await userController.GetUsers();
            var contentResult = result.Result as OkObjectResult;
            var resultUsers = contentResult.Value as UserDto[];

            //Assert
            Assert.True(resultUsers.Length > 0);
        }

        [Fact]
        public async void GetById_IfExist_ExpectedNotNull()
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
        public async void GetByName_IfExist_ExpectedNotNull()
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
        }
        [Fact]
        public async void GetByEmail_IfExist_ExpectedNotNull()
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
        }

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
