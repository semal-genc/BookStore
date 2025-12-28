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
            var book = dbContext.Books.AsNoTracking().Include(x => x.Genre).Include(x => x.Author).SingleOrDefault(x => x.Id == BookId) ??
                throw new InvalidOperationException("Kitap bulunamadı!");

            return _mapper.Map<GetBookDetailViewModel>(book);
        }
    }

    public class GetBookDetailViewModel
    {
        public string Title { get; set; } = null!;
        public int PageCount { get; set; }
        public DateOnly PublishDate { get; set; }

        public string Genre { get; set; } = null!;
        public string AuthorFullName { get; set; } = null!;
    }
}