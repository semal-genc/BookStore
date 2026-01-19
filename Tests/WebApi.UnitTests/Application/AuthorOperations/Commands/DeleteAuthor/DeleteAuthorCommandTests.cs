using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAuthorIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = new DeleteAuthorCommand(_context);
            command.AuthorId = 999;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Silmek istediğiniz yazar bulunamadı");
        }

        [Fact]
        public void WhenAuthorHasBooks_InvalidOperationException_ShouldBeThrown()
        {
            var author = _context.Authors
                .Include(x => x.Books)
                .First(x => x.Books.Any());

            var command = new DeleteAuthorCommand(_context);
            command.AuthorId = author.Id;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yazara ait kitaplar silinmeden yazar silinemez.");
        }

        [Fact]
        public void WhenAuthorHasNoBooks_Author_ShouldBeDeleted()
        {
            var author = new Author
            {
                Name = "Test",
                Surname = "Author",
                BirthDate = new DateOnly(1980, 1, 1),
                Books = new List<Book>()
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            var command = new DeleteAuthorCommand(_context);
            command.AuthorId = author.Id;

            command.Handle();

            _context.Authors.SingleOrDefault(x => x.Id == author.Id).Should().BeNull();
        }
    }
}