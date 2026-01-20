using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Users
    {
        public static void AddUsers(this BookStoreDbContext context)
        {
            context.Users.AddRange(
                new User
                {
                    Id = 1,
                    Email = "test@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Name = "Test",
                    Surname = "User",
                    RefreshToken = "test_refresh_token",
                    RefreshTokenExpireDate = DateTime.Now.AddDays(7)
                }
            );
        }
    }
}