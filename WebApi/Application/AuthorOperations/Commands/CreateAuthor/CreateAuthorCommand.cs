using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public required CreateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var authorExists = _context.Authors.Any(x => x.Name.ToLower() == Model.Name.Trim().ToLower() &&
                x.Surname.ToLower() == Model.Surname.Trim().ToLower());
            if (authorExists)
                throw new InvalidOperationException("Yazar zaten mevcut.");

            var author = _mapper.Map<Author>(Model);

            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}