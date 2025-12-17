using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetByIdBooksQuery
    {
        public int BookId { get; set; }

        private readonly BookStoreDbContext dbContext;

        public GetByIdBooksQuery(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public GetByIdBooksViewModel Handle()
        {
            var book = dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");

            return new GetByIdBooksViewModel
            {
                Title = book.Title,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate,
                Genre = ((GenreEnum)book.GenreId).ToString()
            };
        }
    }

    public class GetByIdBooksViewModel
    {
        public required string Title { get; set; }
        public int PageCount { get; set; }
        public DateOnly PublishDate { get; set; }
        public string? Genre { get; set; }
    }
}