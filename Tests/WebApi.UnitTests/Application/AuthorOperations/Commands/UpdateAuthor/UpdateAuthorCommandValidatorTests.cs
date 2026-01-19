using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenAuthorIdIsInvalid_Validator_ShouldReturnError(int authorId)
        {
            var command = new UpdateAuthorCommand(null!, null!)
            {
                AuthorId = authorId,
                Model = new UpdateAuthorModel()
            };

            var validator = new UpdateAuthorCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "AuthorId");
        }

        [Fact]
        public void WhenNameIsShorterThan4Characters_Validator_ShouldReturnError()
        {
            var command = new UpdateAuthorCommand(null!, null!)
            {
                AuthorId = 1,
                Model = new UpdateAuthorModel
                {
                    Name = "Ab"
                }
            };

            var validator = new UpdateAuthorCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Name");
        }

        [Fact]
        public void WhenSurnameIsShorterThan4Characters_Validator_ShouldReturnError()
        {
            var command = new UpdateAuthorCommand(null!, null!)
            {
                AuthorId = 1,
                Model = new UpdateAuthorModel
                {
                    Surname = "Ab"
                }
            };

            var validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.Surname");
        }

        [Fact]
        public void WhenBirthDateIsToday_Validator_ShouldReturnError()
        {
            var command = new UpdateAuthorCommand(null!, null!)
            {
                AuthorId = 1,
                Model = new UpdateAuthorModel
                {
                    BirthDate = DateOnly.FromDateTime(DateTime.Today)
                }
            };

            var validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().Contain(x => x.PropertyName == "Model.BirthDate");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            var command = new UpdateAuthorCommand(null!, null!)
            {
                AuthorId = 1,
                Model = new UpdateAuthorModel
                {
                    Name = "Ahmet",
                    Surname = "Yılmaz",
                    BirthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-30))
                }
            };

            var validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}