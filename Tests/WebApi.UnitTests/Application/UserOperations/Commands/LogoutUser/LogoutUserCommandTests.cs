using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using WebApi.Application.UserOperations.Commands.LogoutUser;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.UserOperations.Commands.LogoutUser
{
    public class LogoutUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public LogoutUserCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenUserIdClaimDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var httpContext = new DefaultHttpContext();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var command = new LogoutUserCommand(_context, httpContextAccessorMock.Object);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kullanıcı Bulunamadı.");
        }

        [Fact]
        public void WhenUserDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "999")
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var command = new LogoutUserCommand(_context, httpContextAccessorMock.Object);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kullanıcı Bulunamadı.");
        }

        [Fact]
        public void WhenValidUserIsGiven_RefreshToken_ShouldBeCleared()
        {
            var user = new User
            {
                Name = "Test",
                Surname = "User",
                Email = "test@test.com",
                PasswordHash = "hashed",
                RefreshToken = "refresh-token",
                RefreshTokenExpireDate = DateTime.Now.AddMinutes(10)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var command = new LogoutUserCommand(_context, httpContextAccessorMock.Object);

            command.Handle();

            var updatedUser = _context.Users.Single(x => x.Id == user.Id);

            updatedUser.RefreshToken.Should().BeNull();
            updatedUser.RefreshTokenExpireDate.Should().BeNull();
        }
    }
}