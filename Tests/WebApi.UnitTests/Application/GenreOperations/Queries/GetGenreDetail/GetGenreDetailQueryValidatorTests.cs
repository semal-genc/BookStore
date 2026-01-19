using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldReturnErrors(int genreId)
        {
            var query = new GetGenreDetailQuery(null!, null!)
            {
                GenreId = genreId
            };

            var validator = new GetGenreDetailQueryValidator();

            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError()
        {
            var query=new GetGenreDetailQuery(null!, null!)
            {
                GenreId=1
            };

            var validator =new GetGenreDetailQueryValidator();

            var result=validator.Validate(query);

            result.Errors.Should().BeEmpty();
        }
    }
}