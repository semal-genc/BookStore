using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext dbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var book=dbContext.Books.Where(x=>x.Id==BookId).SingleOrDefault();
            if(book is null)
            throw new InvalidOperationException("Silinecek kitap bulunamadı!");

            dbContext.Books.Remove(book);
            dbContext.SaveChanges();
        }
    }
}