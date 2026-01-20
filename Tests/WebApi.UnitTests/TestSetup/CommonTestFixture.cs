using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; private set; }

        public CommonTestFixture()
        {
            var settings = new Dictionary<string, string>
            {
                { "Token:Issuer", "TestIssuer" },
                { "Token:Audience", "TestAudience" },
                { "Token:SecurityKey", "ThisIsASecretKeyForUnitTest123456!" }
            };

            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings!)
                .Build();

            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            Context = new BookStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddUsers();
            Context.AddAuthors();
            Context.AddGenres();
            Context.AddBooks();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}