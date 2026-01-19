using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGenreDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context)
            {
                GenreId = 999,
                Model = new UpdateGenreModel
                {
                    Name = "Updated Name",
                    IsActive = true
                }
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap türü bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            var genre = new Genre
            {
                Name = "Fantasy",
                IsActive = true
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context)
            {
                GenreId = genre.Id,
                Model = new UpdateGenreModel
                {
                    Name = "Epic Fantasy",
                    IsActive = false
                }
            };

            command.Handle();

            var updatedGenre = _context.Genres.Single(x => x.Id == genre.Id);

            updatedGenre.Name.Should().Be("Epic Fantasy");
            updatedGenre.IsActive.Should().BeFalse();
        }

        [Fact]
        public void WhenSameGenreNameIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            var genre=new Genre
            {
                Name="Fantasy",
                IsActive=true
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command=new UpdateGenreCommand(_context)
            {
                GenreId=genre.Id,
                Model=new UpdateGenreModel
                {
                    Name="       fantasy    ",
                    IsActive=true
                }
            };

            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Aynı isimli kitap türü zaten mevcut");
        }
    }
}