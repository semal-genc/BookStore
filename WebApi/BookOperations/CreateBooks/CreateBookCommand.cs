using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public required CreateBookModel Model { get; set; }
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext dbContext;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            if (dbContext.Books.Any(x => x.Title.ToLower() == Model.Title.ToLower()))
                throw new InvalidOperationException("Kitap zaten mevcut.");

            var book = _mapper.Map<Book>(Model);

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