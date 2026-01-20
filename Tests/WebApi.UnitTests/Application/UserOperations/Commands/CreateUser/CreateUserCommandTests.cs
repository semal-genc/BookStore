using AutoMapper;
using FluentAssertions;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistEmailIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            var user = new User
            {
                Name = "Test",
                Surname = "User",
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var command = new CreateUserCommand(_context, _mapper)
            {
                Model = new CreateUserModel
                {
                    Name = "Another",
                    Surname = "User",
                    Email = "test@test.com",
                    Password = "123456"
                }
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Bu email adresi zaten kayıtlı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeCreated()
        {
            var command = new CreateUserCommand(_context, _mapper)
            {
                Model = new CreateUserModel
                {
                    Name = "Semal",
                    Surname = "Genç",
                    Email = "semal@test.com",
                    Password = "123456"
                }
            };

            command.Handle();

            var user = _context.Users.SingleOrDefault(x => x.Email == command.Model.Email);

            user.Should().NotBeNull();
            user!.Name.Should().Be(command.Model.Name);
            user.Surname.Should().Be(command.Model.Surname);
        }

        [Fact]
        public void WhenUserIsCreated_Password_ShouldBeHashed()
        {
            var command = new CreateUserCommand(_context, _mapper)
            {
                Model = new CreateUserModel
                {
                    Name = "Semal",
                    Surname = "Genç",
                    Email = "hash@test.com",
                    Password = "123456"
                }
            };

            command.Handle();

            var user = _context.Users.Single(x => x.Email == command.Model.Email);

            user.PasswordHash.Should().NotBe(command.Model.Password);
            BCrypt.Net.BCrypt.Verify(command.Model.Password, user.PasswordHash).Should().BeTrue();
        }

        [Fact]
        public void WhenUserIsCreated_UserCount_ShouldIncrease()
        {
            var usersCountBefore = _context.Users.Count();

            var command = new CreateUserCommand(_context, _mapper)
            {
                Model = new CreateUserModel
                {
                    Name = "Count",
                    Surname = "Test",
                    Email = "count@test.com",
                    Password = "123456"
                }
            };

            command.Handle();

            _context.Users.Count().Should().Be(usersCountBefore + 1);
        }
    }
}