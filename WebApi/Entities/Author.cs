using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}