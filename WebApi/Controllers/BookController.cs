using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBooks;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBookQuery query = new GetBookQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<GetByIdBooksViewModel> GetById(int id)
        {
            var query = new GetByIdBooksQuery(_context)
            {
                BookId = id
            };

            var book = query.Handle();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context)
            {
                Model = newBook
            };

            try
            {
                command.Handle();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("", null);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel model)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context)
            {
                Model = model
            };
            command.BookId = id;
            try
            {
                command.Handle();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();
            _context.Books.Remove(book);

            _context.SaveChanges();
            return Ok();
        }
    }
}