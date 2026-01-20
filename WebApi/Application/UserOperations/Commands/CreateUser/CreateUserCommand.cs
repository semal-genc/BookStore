using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public required CreateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var userExists = _context.Users.Any(x => x.Email == Model.Email);
            if (userExists)
                throw new InvalidOperationException("Bu email adresi zaten kayıtlı.");

            var user = _mapper.Map<User>(Model);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Model.Password);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

    public class CreateUserModel
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}