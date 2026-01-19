using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGenreAlreadyExists_InvalidOperationException_ShouldBeThrown()
        {
            var genre = new Genre { Name = "Fantasy" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context)
            {
                Model = new CreateGenreModel
                {
                    Name = "Fantasy"
                }
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap türü zaten mevcut!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            var uniqueName = Guid.NewGuid().ToString();
            CreateGenreCommand command = new CreateGenreCommand(_context)
            {
                Model = new CreateGenreModel
                {
                    Name = uniqueName
                }
            };

            command.Handle();

            var genre = _context.Genres.SingleOrDefault(x => x.Name == uniqueName);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(uniqueName);
        }
    }
}