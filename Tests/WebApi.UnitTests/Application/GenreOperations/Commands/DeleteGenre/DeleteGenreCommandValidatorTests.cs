
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests
    {
        [Fact]
        public void WhenGenreIdIsZero_Validator_ShouldReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null!);
            command.GenreId = 0;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGenreIdIsNegative_Validator_ShouldReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null!);
            command.GenreId = -1;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGenreIdIsGreaterThanZero_Validator_ShouldNotReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null!);
            command.GenreId = 1;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}