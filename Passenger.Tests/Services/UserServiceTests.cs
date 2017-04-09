using System.Threading.Tasks;
using Passenger.Infrastructure.Services;
using Xunit;
using Moq;
using Passenger.Core.Repositories;
using AutoMapper;
using Passenger.Core.Domain;

namespace Passenger.Tests.ServicesmapperMock
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);
            await userService.RegisterAsync("user@email.com", "user", "secret");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task when_calling_get_async_and_user_exists()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);
            await userService.GetAsync("user1@email.com");

            userRepositoryMock
            .Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync();
        }
    }
}