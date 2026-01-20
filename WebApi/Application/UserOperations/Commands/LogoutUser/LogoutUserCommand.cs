using System.Security.Claims;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.LogoutUser
{
    public class LogoutUserCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutUserCommand(IBookStoreDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Handle()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                throw new InvalidOperationException("Kullanıcı Bulunamadı.");

            int userId = int.Parse(userIdClaim.Value);
            var user = _context.Users.SingleOrDefault(x => x.Id == userId);

            if (user is null)
                throw new InvalidOperationException("Kullanıcı Bulunamadı.");

            user.RefreshToken = null;
            user.RefreshTokenExpireDate = null;

            _context.SaveChanges();
        }
    }
}