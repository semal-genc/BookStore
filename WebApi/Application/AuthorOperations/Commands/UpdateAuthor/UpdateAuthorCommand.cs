using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public required UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId) ??
                throw new InvalidOperationException("Güncellemek istediğiniz yazar bulunamadı");

            _mapper.Map(Model, author);

            if (!string.IsNullOrWhiteSpace(author.Name))
                author.Name = author.Name.Trim();

            if (!string.IsNullOrWhiteSpace(author.Surname))
                author.Surname = author.Surname.Trim();

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateOnly? BirthDate { get; set; }
    }
}