using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public required UpdateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _context;

        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId) 
                ?? throw new InvalidOperationException("Kitap türü bulunamadı!");
            if (Model.Name is not null)
            {
                if (string.Equals(genre.Name?.Trim(),Model.Name?.Trim(),StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("Aynı isimli kitap türü zaten mevcut");

                if (!string.IsNullOrWhiteSpace(Model.Name))
                    genre.Name = Model.Name.Trim();
            }

            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}