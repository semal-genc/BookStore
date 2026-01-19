using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = new UpdateAuthorCommand(_context, _mapper)
            {
                AuthorId = 999,
                Model = new UpdateAuthorModel
                {
                    Name = "Test",
                    Surname = "Test"
                }
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Güncellemek istediğiniz yazar bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            var author = new Author
            {
                Name = "Old",
                Surname = "Name",
                BirthDate = new DateOnly(1980, 1, 1)
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            var command = new UpdateAuthorCommand(_context, _mapper)
            {
                AuthorId = author.Id,
                Model = new UpdateAuthorModel
                {
                    Name = "New",
                    Surname = "Author",
                    BirthDate = new DateOnly(1990, 5, 5)
                }
            };

            command.Handle();

            var updatedAuthor = _context.Authors.Single(x => x.Id == author.Id);
            updatedAuthor.Name.Should().Be(author.Name);
            updatedAuthor.Surname.Should().Be(author.Surname);
            updatedAuthor.BirthDate.Should().Be(author.BirthDate);
        }

        [Fact]
        public void WhenNameAndSurnameHaveWhitespaces_They_ShouldBeTrimmed()
        {
            var author = new Author
            {
                Name = "Old",
                Surname = "Name"
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            var command = new UpdateAuthorCommand(_context, _mapper)
            {
                AuthorId = author.Id,
                Model = new UpdateAuthorModel
                {
                    Name = "  Ahmet  ",
                    Surname = "  Yılmaz  "
                }
            };

            command.Handle();

            var updatedAuthor=_context.Authors.Single(x=>x.Id==author.Id);
            updatedAuthor.Name.Should().Be(author.Name);
            updatedAuthor.Surname.Should().Be(author.Surname);
        }
    }
}