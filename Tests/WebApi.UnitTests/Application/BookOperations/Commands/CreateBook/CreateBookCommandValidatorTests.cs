using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0, 0)]
        [InlineData("Lord Of The Rings", 0, 0, 1)]
        [InlineData("Lord Of The Rings", 0, 1, 1)]
        [InlineData("Lord Of The Rings", 0, 1, 0)]
        [InlineData("Lord Of The Rings", 100, 0, 0)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 1, 2, 0)]
        [InlineData("", 1, 0, 1)]
        [InlineData("", 0, 1, 1)]
        [InlineData("", 0, 0, 1)]
        [InlineData("", 0, 1, 0)]
        [InlineData("Lor", 0, 0, 1)]
        [InlineData("Lor", 0, 1, 0)]
        [InlineData("Lor", 0, 1, 1)]
        [InlineData("Lor", 100, 0, 0)]
        [InlineData("Lor", 100, 1, 1)]
        [InlineData("Lord", 0, 0, 1)]
        [InlineData("Lord", 100, 0, 0)]
        [InlineData("Lord", 0, 1, 0)]
        [InlineData("Lord", 0, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_SholdBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            CreateBookCommand command = new CreateBookCommand(default!, default!)
            {
                Model = new CreateBookModel
                {
                    Title = title,
                    PageCount = pageCount,
                    PublishDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                    GenreId = genreId,
                    AuthorId = authorId
                }
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_SholdBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(default!, default!)
            {
                Model = new CreateBookModel
                {
                    Title = "Lord Of The Rings",
                    PageCount = 100,
                    PublishDate = DateOnly.FromDateTime(DateTime.Now),
                    GenreId = 1,
                    AuthorId = 1
                }
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_SholdNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(default!, default!)
            {
                Model = new CreateBookModel
                {
                    Title = "Lord Of The Rings",
                    PageCount = 100,
                    PublishDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-2)),
                    GenreId = 1,
                    AuthorId = 1
                }
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }
    }
}