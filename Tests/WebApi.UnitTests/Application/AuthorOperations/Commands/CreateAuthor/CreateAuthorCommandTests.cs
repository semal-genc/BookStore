using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorAlreadyExists_InvalidOperationException_ShouldBeThrown()
        {
            var author = new Author
            {
                Name = "George",
                Surname = "Orwell",
                BirthDate = new DateOnly(1903, 6, 25)
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper)
            {
                Model = new CreateAuthorModel
                {
                    Name = "George",
                    Surname = "Orwell",
                    BirthDate = new DateOnly(1903, 6, 25)
                }
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yazar zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper)
            {
                Model = new CreateAuthorModel
                {
                    Name = "J.R.R.",
                    Surname = "Tolkien",
                    BirthDate = new DateOnly(1892, 1, 3)
                }
            };

            command.Handle();

            var author = _context.Authors.SingleOrDefault(x => x.Name == "J.R.R." && x.Surname == "Tolkien");

            author.Should().NotBeNull();
            author!.BirthDate.Should().Be(new DateOnly(1892, 1, 3));
        }
    }
}