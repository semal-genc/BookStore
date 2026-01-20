using FluentAssertions;
using WebApi.Application.UserOperations.Commands.RefreshToken;

namespace WebApi.UnitTests.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandValidatorTests
    {
        [Fact]
        public void WhenRefreshTokenIsEmpty_Validator_ShouldReturnError()
        {
            var command = new RefreshTokenCommand(null!, null!);
            command.RefreshToken = "";

            var validator = new RefreshTokenCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenRefreshTokenIsNull_Validator_ShouldReturnError()
        {
            var command = new RefreshTokenCommand(null!, null!);
            command.RefreshToken = null;

            var validator = new RefreshTokenCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenRefreshTokenIsValid_Validator_ShouldNotReturnError()
        {
            var command = new RefreshTokenCommand(null!, null!);
            command.RefreshToken = "valid_refresh_token";

            var validator = new RefreshTokenCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}