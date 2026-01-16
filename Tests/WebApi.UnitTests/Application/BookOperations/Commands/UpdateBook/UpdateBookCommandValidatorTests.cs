using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        [Fact]
        public void WhenBookIdIsZeroOrLess_Validator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(default!, default!)
            {
                BookId = 0,
                Model = new UpdateBookModel()
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenTitleLengthLessThanFour_Validator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(default!, default!)
            {
                BookId = 1,
                Model = new UpdateBookModel
                {
                    Title = "abc"
                }
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPageCountIsZeroOrLess_Validator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(default!, default!)
            {
                BookId = 1,
                Model = new UpdateBookModel
                {
                    PageCount = 0
                }
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPublishDateIsToday_Validator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(default!, default!)
            {
                BookId = 1,
                Model = new UpdateBookModel
                {
                    PublishDate = DateOnly.FromDateTime(DateTime.Today)
                }
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenOnlyBookIdIsGiven_Validator_ShouldNotReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(default!, default!)
            {
                BookId = 1,
                Model = new UpdateBookModel()
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(default!, default!)
            {
                BookId = 1,
                Model = new UpdateBookModel
                {
                    Title = "Updated Title",
                    PageCount = 200,
                    PublishDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                    GenreId = 1,
                    AuthorId = 1
                }
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }
    }
}