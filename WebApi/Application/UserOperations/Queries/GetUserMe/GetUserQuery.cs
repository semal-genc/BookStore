using System.Security.Claims;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Queries.GetUserMe
{
    public class GetUserMeQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserMeQuery(IBookStoreDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public GetUserMeModel Handle()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                throw new InvalidOperationException("Kullanıcı bilgisi bulunamadı.");

            int userId = int.Parse(userIdClaim.Value);

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user is null)
                throw new InvalidOperationException("Kullanıcı bulunamadı.");

            return new GetUserMeModel
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }

    public class GetUserMeModel
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}