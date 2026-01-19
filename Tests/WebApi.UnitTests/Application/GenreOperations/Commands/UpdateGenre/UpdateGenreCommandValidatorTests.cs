using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldReturnError(int genreId)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null!)
            {
                GenreId = genreId,
                Model = new UpdateGenreModel()
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("   ")]
        public void WhenNameIsOnlyWhitespace_Validator_ShouldReturnError(string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null!)
            {
                GenreId = 1,
                Model = new UpdateGenreModel
                {
                    Name = name
                }
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Abc")]
        [InlineData("Ab")]
        [InlineData("A")]
        public void WhenNameLengthIsLessThan4_Validator_ShouldReturnError(string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null!)
            {
                GenreId = 1,
                Model = new UpdateGenreModel
                {
                    Name = name
                }
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("Fantasy")]
        [InlineData("Science Fiction")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(string? name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null!)
            {
                GenreId = 1,
                Model = new UpdateGenreModel
                {
                    Name = name
                }
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }
    }
}