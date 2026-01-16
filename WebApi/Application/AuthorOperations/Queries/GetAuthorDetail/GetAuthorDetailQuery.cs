using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetAuthorDetailModel Handle()
        {
            var author = _context.Authors.AsNoTracking().SingleOrDefault(x => x.Id == AuthorId) ?? 
                throw new InvalidOperationException("Yazar bulunamadı!");
            return _mapper.Map<GetAuthorDetailModel>(author);
        }
    }

    public class GetAuthorDetailModel
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
    }
}