using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("123")]
        [InlineData("abc")]
        public void WhenInvalidNameIsGiven_Validator_ShouldReturnErrors(string name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null!)
            {
                Model = new CreateGenreModel
                {
                    Name = name
                }
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidNameIsGiven_Validator_ShouldNotReturnError()
        {
            CreateGenreCommand command = new CreateGenreCommand(null!)
            {
                Model = new CreateGenreModel
                {
                    Name = "Fantasy"
                }
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }
    }
}