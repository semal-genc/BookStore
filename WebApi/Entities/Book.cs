using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Title { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public int PageCount { get; set; }
        public DateOnly PublishDate { get; set; }
    }
}