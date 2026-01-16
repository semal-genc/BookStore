using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author { Name = "Eric", Surname = "Ries", BirthDate = new DateOnly(1978, 9, 22) },
                new Author { Name = "Charlotte", Surname = "Gilman", BirthDate = new DateOnly(1860, 7, 3) },
                new Author { Name = "Frank", Surname = "Herbert", BirthDate = new DateOnly(1920, 10, 8) },
                new Author { Name = "J.K.", Surname = "Rowling", BirthDate = new DateOnly(1965, 7, 31) },
                new Author { Name = "George", Surname = "Orwell", BirthDate = new DateOnly(1903, 6, 25) }
            );
        }
    }
}