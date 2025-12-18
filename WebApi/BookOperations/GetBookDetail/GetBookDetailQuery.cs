using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }

        private readonly BookStoreDbContext dbContext;

        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public GetBookDetailViewModel Handle()
        {
            var book = dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");

            return new GetBookDetailViewModel
            {
                Title = book.Title,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate,
                Genre = ((GenreEnum)book.GenreId).ToString()
            };
        }
    }

    public class GetBookDetailViewModel
    {
        public required string Title { get; set; }
        public int PageCount { get; set; }
        public DateOnly PublishDate { get; set; }
        public string? Genre { get; set; }
    }
}