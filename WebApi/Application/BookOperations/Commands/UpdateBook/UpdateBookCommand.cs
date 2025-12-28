using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public required UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Handle()
        {
            var book = dbContext.Books.SingleOrDefault(x => x.Id == BookId) ??
                throw new InvalidOperationException("Kitap bulunamadı!");

            if (!string.IsNullOrWhiteSpace(Model.Title))
            {
                var title = Model.Title.Trim();
                var exists = dbContext.Books
                    .Any(x => x.Title.ToLower() == title.ToLower() && x.Id != BookId);

                if (exists)
                    throw new InvalidOperationException("Aynı isimde başka bir kitap mevcut.");
            }
            if (Model.GenreId.HasValue)
            {
                var genreExists = dbContext.Genres
                    .Any(x => x.Id == Model.GenreId.Value && x.IsActive);

                if (!genreExists)
                    throw new InvalidOperationException("Tür bulunamadı.");
            }
            if (Model.AuthorId.HasValue)
            {
                var authorExists = dbContext.Authors
                    .Any(x => x.Id == Model.AuthorId);

                if (!authorExists)
                    throw new InvalidOperationException("Yazar bulunamadı.");
            }

            mapper.Map(Model, book);

            dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string? Title { get; set; }
        public int? PageCount { get; set; }
        public DateOnly? PublishDate { get; set; }
        public int? GenreId { get; set; }
        public int? AuthorId { get; set; }
    }
}