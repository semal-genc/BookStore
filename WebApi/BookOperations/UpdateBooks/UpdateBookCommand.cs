using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBooks
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public required UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var book = dbContext.Books.Where(x => x.Id == BookId).FirstOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");

            if (!string.IsNullOrWhiteSpace(Model.Title))
                book.Title = Model.Title;
            if (Model.PageCount > 0)
                book.PageCount = Model.PageCount.Value;
            if (Model.PublishDate != default)
                book.PublishDate = Model.PublishDate.Value;
            if (Model.GenreId > 0)
                book.GenreId = Model.GenreId.Value;

            dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string? Title { get; set; }
        public int? PageCount { get; set; }
        public DateOnly? PublishDate { get; set; }
        public int? GenreId { get; set; }
    }
}