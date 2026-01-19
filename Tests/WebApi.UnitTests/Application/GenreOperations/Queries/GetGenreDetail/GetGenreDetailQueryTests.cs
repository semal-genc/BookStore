using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGenreDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_mapper, _context);
            query.GenreId = 999;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap türü bulunamadı!");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeReturned()
        {
            var genre = _context.Genres.First();

            GetGenreDetailQuery query = new GetGenreDetailQuery(_mapper, _context);
            query.GenreId = genre.Id;

            var result = query.Handle();

            result.Should().NotBeNull();
            result.Id.Should().Be(genre.Id);
            result.Name.Should().Be(genre.Name);
        }
    }
}