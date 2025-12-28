using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any() || context.Authors.Any() || context.Genres.Any())
                    return;

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personel Growth",
                    },
                    new Genre
                    {
                        Name = "Science Fiction",
                    },
                    new Genre
                    {
                        Name = "Romance",
                    }
                );
                context.SaveChanges();

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,
                        AuthorId = 4,
                        PageCount = 200,
                        PublishDate = new DateOnly(2001, 06, 12)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        AuthorId = 2,
                        PageCount = 250,
                        PublishDate = new DateOnly(2011, 05, 23)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2,
                        AuthorId = 3,
                        PageCount = 540,
                        PublishDate = new DateOnly(2025, 09, 02)
                    }
                );
                context.SaveChanges();

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Eric",
                        Surname = "Ries",
                        BirthDate = new DateOnly(1978, 9, 22)
                    },
                    new Author
                    {
                        Name = "Charlotte",
                        Surname = "Gilman",
                        BirthDate = new DateOnly(1860, 7, 3)
                    },
                    new Author
                    {
                        Name = "Frank",
                        Surname = "Herbert",
                        BirthDate = new DateOnly(1920, 10, 8)
                    },
                    new Author
                    {
                        Name = "J.K.",
                        Surname = "Rowling",
                        BirthDate = new DateOnly(1965, 7, 31)
                    },
                    new Author
                    {
                        Name = "George",
                        Surname = "Orwell",
                        BirthDate = new DateOnly(1903, 6, 25)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}