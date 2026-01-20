using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _configuration = testFixture.Configuration;
        }

        [Fact]
        public void WhenInvalidPasswordIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            var command = new CreateTokenCommand(_context, _mapper, _configuration)
            {
                Model = new CreateTokenModel
                {
                    Email = "test@test.com",
                    Password = "wrongPassword"
                }
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Email veya şifre hatalı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Token_ShouldBeReturned()
        {
            var command = new CreateTokenCommand(_context, _mapper, _configuration)
            {
                Model = new CreateTokenModel
                {
                    Email = "test@test.com",
                    Password = "123456"
                }
            };

            var token = command.Handle();

            token.Should().NotBeNull();
            token.AccessToken.Should().NotBeEmpty();
            token.RefreshToken.Should().NotBeEmpty();
            token.Expiration.Should().BeAfter(DateTime.Now);
        }

        [Fact]
        public void WhenTokenIsCreated_RefreshToken_ShouldBeSavedToUser()
        {
            var command = new CreateTokenCommand(_context, _mapper, _configuration)
            {
                Model = new CreateTokenModel
                {
                    Email = "test@test.com",
                    Password = "123456"
                }
            };

            var token = command.Handle();
            var user = _context.Users.Single(x => x.Email == command.Model.Email);

            user.RefreshToken.Should().Be(token.RefreshToken);
            user.RefreshTokenExpireDate.Should().NotBeNull();
        }
    }
}