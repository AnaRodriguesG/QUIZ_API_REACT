using Microsoft.AspNetCore.Mvc;
using QUIZ_API_REACT.Context;
using QUIZ_API_REACT.Entities;
using QUIZ_API_REACT.Utilities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QUIZ_API_REACT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly QuizContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UsersController(QuizContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest("Password is required");

            _passwordHasher.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Nome = userDto.Nome,
                Email = userDto.Email,
                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = Convert.ToBase64String(passwordSalt)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
                return Unauthorized();

            if (!_passwordHasher.VerifyPasswordHash(loginDto.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
                return Unauthorized();

            return Ok(user);
        }
    }

    public class UserRegisterDto
    {
        public string Nome { get; set; }
        public int FotoPerfil { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
