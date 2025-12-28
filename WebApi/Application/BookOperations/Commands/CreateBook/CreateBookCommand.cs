using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
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

            var authorExists = dbContext.Authors.Any(x => x.Id == Model.AuthorId);
            if (!authorExists)
                throw new InvalidOperationException("Yazar bulunamadı.");

            var genreExists = dbContext.Genres.Any(x => x.Id == Model.GenreId && x.IsActive);
            if (!authorExists)
                throw new InvalidOperationException("Tür bulunamadı.");

            var book = _mapper.Map<Book>(Model);

            dbContext.Books.Add(book);
            dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; } = null!;
        public int PageCount { get; set; }
        public DateOnly PublishDate { get; set; }

        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}