using FluentAssertions;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _configuration = testFixture.Configuration;
        }

        [Fact]
        public void WhenRefreshTokenIsInvalid_InvalidOperationException_ShouldBeThrown()
        {
            var command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = "invalid_refresh_token";

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Geçerli bir refresh token bulunamadı.");
        }

        [Fact]
        public void WhenRefreshTokenIsValid_Token_ShouldBeReturned()
        {
            var user = new User
            {
                Name = "Test",
                Surname = "User",
                Email = "test@test.com",
                PasswordHash = "123456",
                RefreshToken = "valid_refresh_token",
                RefreshTokenExpireDate = DateTime.Now.AddMinutes(10)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = "valid_refresh_token";

            var token = command.Handle();

            token.Should().NotBeNull();
            token.AccessToken.Should().NotBeNullOrEmpty();
            token.RefreshToken.Should().NotBeNullOrEmpty();

            var updatedUser = _context.Users.Single(x => x.Id == user.Id);
            updatedUser.RefreshToken.Should().Be(token.RefreshToken);
            updatedUser.RefreshTokenExpireDate.Should().BeAfter(DateTime.Now);
        }
    }
}