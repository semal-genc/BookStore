using FluentAssertions;
using WebApi.Application.UserOperations.Commands.CreateToken;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidatorTests
    {
        [Fact]
        public void WhenEmailIsEmpty_Validator_ShouldHaveError()
        {
            var command = new CreateTokenCommand(null!, null!, null!)
            {
                Model = new CreateTokenModel
                {
                    Email = "",
                    Password = "123456"
                }
            };

            var validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Email");
        }

        [Fact]
        public void WhenEmailFormatIsInvalid_Validator_ShouldHaveError()
        {
            var command = new CreateTokenCommand(null!, null!, null!)
            {
                Model = new CreateTokenModel
                {
                    Email = "invalidEmail",
                    Password = "123456"
                }
            };

            var validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Email");
        }

        [Fact]
        public void WhenPasswordIsEmpty_Validator_ShouldHaveError()
        {
            var command = new CreateTokenCommand(null!, null!, null!)
            {
                Model = new CreateTokenModel
                {
                    Email = "test@test.com",
                    Password = ""
                }
            };

            var validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Password");
        }

        [Fact]
        public void WhenEmailAndPasswordAreEmpty_Validator_ShouldHaveErrors()
        {
            var command = new CreateTokenCommand(null!, null!, null!)
            {
                Model = new CreateTokenModel
                {
                    Email = "",
                    Password = ""
                }
            };

            var validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(1);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotHaveError()
        {
            var command = new CreateTokenCommand(null!, null!, null!)
            {
                Model = new CreateTokenModel
                {
                    Email = "test@test.com",
                    Password = "123456"
                }
            };

            var validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }
    }
}