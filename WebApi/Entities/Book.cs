using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
        public int PageCount { get; set; }
        public DateOnly PublishDate { get; set; }
    }
}