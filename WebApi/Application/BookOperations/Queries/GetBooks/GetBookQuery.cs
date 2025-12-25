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
            var bookList = DbContext.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);

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