using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookDoesNotExist_ShouldThrowInvalidOperationException()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper)
            {
                Model = new UpdateBookModel
                {
                    Title = "Updated Title"
                }
            };
            command.BookId = 999;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            var book = new Book
            {
                Title = "Old Title",
                PageCount = 100,
                PublishDate = new DateOnly(2000, 1, 1),
                GenreId = 1,
                AuthorId = 1
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper)
            {
                Model = new UpdateBookModel
                {
                    Title = "Updated Title",
                    PageCount = 250
                }
            };
            command.BookId = book.Id;

            command.Handle();

            var updatedBook = _context.Books.SingleOrDefault(x => x.Id == book.Id);

            updatedBook.Should().NotBeNull();
            updatedBook!.Title.Should().Be("Updated Title");
            updatedBook.PageCount.Should().Be(250);
        }
    }
}