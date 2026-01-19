using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData("", "", "2000-01-01")]
        [InlineData("Geo", "Orwell", "2000-01-01")]
        [InlineData("George", "Orw", "2000-01-01")]
        [InlineData("", "Orwell", "2000-01-01")]
        [InlineData("George", "", "2000-01-01")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string surname, string birthDate)
        {
            var command = new CreateAuthorCommand(null!, null!)
            {
                Model = new CreateAuthorModel
                {
                    Name = name,
                    Surname = surname,
                    BirthDate = DateOnly.Parse(birthDate)
                }
            };

            var validator = new CreateAuthorCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenBirthDateIsTodayOrInFuture_Validator_ShouldReturnError()
        {
            var command = new CreateAuthorCommand(null!, null!)
            {
                Model = new CreateAuthorModel
                {
                    Name = "George",
                    Surname = "Orwell",
                    BirthDate = DateOnly.FromDateTime(DateTime.Today)
                }
            };

            var validator = new CreateAuthorCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            var command = new CreateAuthorCommand(null!, null!)
            {
                Model = new CreateAuthorModel
                {
                    Name = "George",
                    Surname = "Orwell",
                    BirthDate = new DateOnly(1903, 6, 25)
                }
            };

            var validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }
    }
}