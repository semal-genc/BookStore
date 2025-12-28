using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.GetBooks
{
    public class GetBookQuery
    {
        private readonly BookStoreDbContext DbContext;
        private readonly IMapper _mapper;
        public GetBookQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = DbContext.Books.AsNoTracking().Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id).ToList<Book>();
            return _mapper.Map<List<BooksViewModel>>(bookList);
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; } = null!;
        public int PageCount { get; set; }
        public DateOnly PublishDate { get; set; }

        public string Genre { get; set; } = null!;
        public string AuthorFullName { get; set; } = null!;
    }
}