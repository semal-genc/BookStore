using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookQuery
    {
        private readonly BookStoreDbContext DbContext;
        public GetBookQuery(BookStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = DbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();

            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate,
                    PageCount = book.PageCount
                });
            }
            return vm;
        }
    }

    public class BooksViewModel
    {
        public required string Title { get; set; }
        public int PageCount { get; set; }
        public DateOnly PublishDate { get; set; }
        public string? Genre { get; set; }
    }
}