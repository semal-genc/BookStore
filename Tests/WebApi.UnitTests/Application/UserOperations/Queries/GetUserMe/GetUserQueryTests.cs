using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using WebApi.Application.UserOperations.Queries.GetUserMe;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.UserOperations.Queries.GetUserMe
{
    public class GetUserMeQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public GetUserMeQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        private IHttpContextAccessor BuildHttpContextAccessor(int? userId)
        {
            var httpContext = new DefaultHttpContext();

            if (userId is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.Value.ToString())
                };

                var identity = new ClaimsIdentity(claims, "TestAuth");
                httpContext.User = new ClaimsPrincipal(identity);
            }

            return new HttpContextAccessor
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public void WhenUserIdClaimIsMissing_InvalidOperationException_ShouldBeThrown()
        {
            var httpContextAccessor = BuildHttpContextAccessor(null);
            var query = new GetUserMeQuery(_context, httpContextAccessor);

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Kullanıcı bilgisi bulunamadı.");
        }

        [Fact]
        public void WhenUserDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var httpContextAccessor = BuildHttpContextAccessor(999);
            var query = new GetUserMeQuery(_context, httpContextAccessor);

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Kullanıcı bulunamadı.");
        }

        [Fact]
        public void WhenValidUserExists_UserInfo_ShouldBeReturned()
        {
            var user = _context.Users.First();
            var httpContextAccessor = BuildHttpContextAccessor(user.Id);

            var query = new GetUserMeQuery(_context, httpContextAccessor);

            var result = query.Handle();

            result.Email.Should().Be(user.Email);
            result.Name.Should().Be(user.Name);
            result.Surname.Should().Be(user.Surname);
        }
    }
}