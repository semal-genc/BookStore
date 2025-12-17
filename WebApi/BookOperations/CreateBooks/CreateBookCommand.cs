using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public required CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext dbContext;

        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            if (dbContext.Books.Any(x => x.Title.ToLower() == Model.Title.ToLower()))
                throw new InvalidOperationException("Kitap zaten mevcut.");

            var book = new Book
            {
                Title = Model.Title,
                PublishDate = Model.PublishDate,
                PageCount = Model.PageCount,
                GenreId = Model.GenreId
            };

            dbContext.Books.Add(book);
            dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public required string Title { get; set; }
        public int PageCount { get; set; }
        public DateOnly PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}