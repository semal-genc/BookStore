
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldReturnErrors(int bookId)
        {
            var query = new GetBookDetailQuery(null!, null!);
            query.BookId = bookId;

            var validator = new GetBookDetailQueryValidator();

            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotReturnError()
        {
            var query = new GetBookDetailQuery(null!, null!);
            query.BookId = 1;

            var validator = new GetBookDetailQueryValidator();

            var result = validator.Validate(query);

            result.Errors.Should().BeEmpty();
        }
    }
}