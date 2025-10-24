using J3m_BE.Data;
using J3m_BE.DTOs.AdminDTOs;
using J3m_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace J3m_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginDTO loginAdmin)
        {
            var admin = _context.Admins.SingleOrDefault(a => a.Username == loginAdmin.Username && a.PasswordHash == loginAdmin.Password);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginAdmin.Password, admin.PasswordHash))
            {
                return Unauthorized("Invalid email or password.");
            }
          
            var token = GenerateJwToken(admin);
            return Ok(new { Token = token });

        }
        [HttpPost("register")]
        public IActionResult Register(AdminRegisterDTO newAdmin)
        {

            //See if an admin is logged in?
            // Do we want a code to register as admin or an existing admin to create new admins?


            // check if email already exists
            if (_context.Admins.Any(a => a.Username == newAdmin.Username))
            {
                return BadRequest("Username already exists.");
            }

            //Hash the password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newAdmin.Password);

            var admin = new Admin
            {
                Username = newAdmin.Username,
                PasswordHash = passwordHash,
               
            };

            _context.Admins.Add(admin);
            _context.SaveChanges();
            return Ok("Admin registered successfully.");
        }


        private string GenerateJwToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, admin.Username),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
