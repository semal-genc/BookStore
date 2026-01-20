using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.Commands.LogoutUser;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.Application.UserOperations.Queries.GetUserMe;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel model)
        {
            var command = new CreateUserCommand(_context, _mapper)
            {
                Model = model
            };
            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel model)
        {
            var command = new CreateTokenCommand(_context, _mapper, _configuration)
            {
                Model = model
            };
            return command.Handle();
        }

        [HttpPost("refresh-token")]
        public ActionResult<Token> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand(_context, _configuration)
            {
                RefreshToken = request.RefreshToken
            };
            return command.Handle();
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetMe()
        {
            var query = new GetUserMeQuery(_context, _httpContextAccessor);
            return Ok(query.Handle());
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var command = new LogoutUserCommand(_context, _httpContextAccessor);
            command.Handle();
            return Ok("Çıkış Yapıldı.");
        }
    }
}