using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetAuthorsModel> Handle()
        {
            var authorList = _context.Authors.AsNoTracking().OrderBy(x => x.Id).ToList();
            return _mapper.Map<List<GetAuthorsModel>>(authorList);
        }
    }

    public class GetAuthorsModel
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
    }
}