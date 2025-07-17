using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YuvamHazir.Domain.Entities;
using YuvamHazir.Infrastructure.Context;
using YuvamHazir.API.DTOs;
using static YuvamHazir.API.DTOs.UserDto;

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(YuvamHazirDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("Bu mail zaten kayıtlı");

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = dto.Password // DİKKAT: Plain text, hashlemen lazım!
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { user.Id, user.FullName, user.Email });
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email && u.PasswordHash == dto.Password);
            if (user == null)
                return Unauthorized("Giriş bilgileri yanlış");

            // TOKEN ÜRET
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new UserLoginResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Role = user.Role,
                Token = tokenStr
            };

            return Ok(response);
        }
    }
}
