using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }

        private readonly BookStoreDbContext dbContext;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public GetBookDetailViewModel Handle()
        {
            var book = dbContext.Books.Include(x => x.Genre).Where(book => book.Id == BookId).SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");

            GetBookDetailViewModel vm = _mapper.Map<GetBookDetailViewModel>(book);

            return vm;
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