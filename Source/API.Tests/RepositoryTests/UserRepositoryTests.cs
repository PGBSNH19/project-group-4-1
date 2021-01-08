using API.Context;
using API.Models;
using API.Services;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace API.Tests.RepositoryTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async void GetAll_IfAnyExist_ReturnTrue()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);

            //Act
            var result = await userRepository.GetUsers();

            //Assert
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async void GetById_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);

            //Act
            var result = await userRepository.GetUserById(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetById_IfNotExist_ExpectedNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);

            //Act
            var result = await userRepository.GetUserById(4);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetByUsername_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);

            //Act
            var result = await userRepository.GetUserByName("Example1");

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetByUsername_IfNotExist_ExpectedNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);

            //Act
            var result = await userRepository.GetUserByName("Klas");

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetByEmail_IfExist_ExpectedNotNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);

            //Act
            var result = await userRepository.GetUserByEmail("example1@examplesson.se");

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetByEmail_IfNotExist_ExpectedNull()
        {
            //Arrange
            var mockContext = new Mock<NearbyProduceContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(GetUsers());
            var userRepository = new UserRepository(mockContext.Object);

            //Act
            var result = await userRepository.GetUserByEmail("WrongMail@exmaple.com");

            //Assert
            Assert.Null(result);
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
