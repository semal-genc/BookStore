using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.Include(x => x.Books).SingleOrDefault(x => x.Id == AuthorId) ??
                throw new InvalidOperationException("Silmek istediğiniz yazar bulunamadı");

            if(author.Books.Any())
                throw new InvalidOperationException("Yazara ait kitaplar silinmeden yazar silinemez.");
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}