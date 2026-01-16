using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldReturnErrors(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(default!);
            command.BookId = bookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotReturnError()
        {
            DeleteBookCommand command = new DeleteBookCommand(default!);
            command.BookId = 1;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Should().BeEmpty();
        }
    }
}