using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookIsNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 999;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_BookDetail_ShouldBeReturned()
        {
            var book = _context.Books.First();

            var query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = book.Id;

            var result = query.Handle();

            result.Should().NotBeNull();
            result.Title.Should().Be(book.Title);
            result.PageCount.Should().Be(book.PageCount);
            result.Genre.Should().Be(book.Genre.Name);
            result.AuthorFullName.Should().Be($"{book.Author.Name} {book.Author.Surname}");
        }
    }
}