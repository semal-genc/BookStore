using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void WhenAuthorIdIsInvalid_Validator_ShouldReturnError(int authorId)
        {
            var query = new GetAuthorDetailQuery(null!, null!)
            {
                AuthorId = authorId
            };

            var validator = new GetAuthorDetailQueryValidator();

            var result = validator.Validate(query);

            result.Errors.Should().Contain(x => x.PropertyName == "AuthorId");
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Validator_ShouldNotReturnError()
        {
            var query = new GetAuthorDetailQuery(null!, null!)
            {
                AuthorId = 1
            };

            var validator = new GetAuthorDetailQueryValidator();

            var result = validator.Validate(query);

            result.Errors.Should().BeEmpty();
        }
    }
}