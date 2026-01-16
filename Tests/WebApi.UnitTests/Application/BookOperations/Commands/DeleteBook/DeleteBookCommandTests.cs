using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenBookIsNotExist_InvalidOperationException_ShouldBeReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 999;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Silinecek kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
        {
            var book = new Book
            {
                Title = "Delete Test Book",
                PageCount = 200,
                PublishDate = new DateOnly(2000, 1, 1),
                GenreId = 1,
                AuthorId = 1
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id;

            command.Handle();

            var deletedBook = _context.Books.SingleOrDefault(x => x.Id == book.Id);
            deletedBook.Should().BeNull();
        }
    }
}