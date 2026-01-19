using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenAuthorIdIsLessThanOrEqualZero_Validator_ShouldReturnError(int authorId)
        {
            var command = new DeleteAuthorCommand(null!);
            command.AuthorId = authorId;

            var validator = new DeleteAuthorCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenAuthorIdIsGreaterThanZero_Validator_ShouldNotReturnError()
        {
            var command = new DeleteAuthorCommand(null!);
            command.AuthorId = 1;

            var validator = new DeleteAuthorCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}