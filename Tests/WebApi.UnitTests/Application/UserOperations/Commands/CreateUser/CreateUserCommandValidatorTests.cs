using FluentAssertions;
using WebApi.Application.UserOperations.Commands.CreateUser;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests
    {
        [Fact]
        public void WhenNameIsEmpty_Validator_ShouldHaveError()
        {
            var command = new CreateUserCommand(null!, null!)
            {
                Model = new CreateUserModel
                {
                    Name = "",
                    Surname = "Test",
                    Email = "test@test.com",
                    Password = "123456"
                }
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Name");
        }

        [Fact]
        public void WhenNameIsLessThanTwoCharacters_Validator_ShouldHaveError()
        {
            var command = new CreateUserCommand(null!, null!)
            {
                Model = new CreateUserModel
                {
                    Name = "A",
                    Surname = "Test",
                    Email = "test@test.com",
                    Password = "123456"
                }
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Name");
        }

        [Fact]
        public void WhenSurnameIsEmpty_Validator_ShouldHaveError()
        {
            var command = new CreateUserCommand(null!, null!)
            {
                Model = new CreateUserModel
                {
                    Name = "Test",
                    Surname = "",
                    Email = "test@test.com",
                    Password = "123456"
                }
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Surname");
        }
        
        [Fact]
        public void WhenEmailIsEmpty_Validator_ShouldHaveError()
        {
            var command = new CreateUserCommand(null!, null!)
            {
                Model = new CreateUserModel
                {
                    Name = "Test",
                    Surname = "User",
                    Email = "",
                    Password = "123456"
                }
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Email");
        }
        
        [Fact]
        public void WhenEmailFormatIsInvalid_Validator_ShouldHaveError()
        {
            var command = new CreateUserCommand(null!, null!)
            {
                Model = new CreateUserModel
                {
                    Name = "Test",
                    Surname = "User",
                    Email = "invalidMail",
                    Password = "123456"
                }
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Email");
        }

        [Fact]
        public void WhenPasswordIsEmpty_Validator_ShouldHaveError()
        {
            var command = new CreateUserCommand(null!, null!)
            {
                Model = new CreateUserModel
                {
                    Name = "Test",
                    Surname = "User",
                    Email = "test@test.com",
                    Password = ""
                }
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Password");
        }

        [Fact]
        public void WhenPasswordIsLessThanSixCharacters_Validator_ShouldHaveError()
        {
            var command = new CreateUserCommand(null!, null!)
            {
                Model = new CreateUserModel
                {
                    Name = "Test",
                    Surname = "User",
                    Email = "test@test.com",
                    Password = "123"
                }
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Password");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotHaveError()
        {
            var command = new CreateUserCommand(null!, null!)
            {
                Model = new CreateUserModel
                {
                    Name = "Semal",
                    Surname = "Genç",
                    Email = "semal@test.com",
                    Password = "123456"
                }
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }
    }
}