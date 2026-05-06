using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using Unity2D.Data;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Unity2D
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(DBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.players.AnyAsync(u => u.Id == dto.Id))
                return BadRequest("This ID already exists.");
            if (await _context.players.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("This Username already exists.");

            var player = new Player
            {
                Name = dto.Name,
                Id = dto.Id,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Username = dto.Username
            };

            _context.players.Add(player);

            var starterItem = new Item_instance
            {
                Iid = 0,
                Id = dto.Id,     
                Eid = 1,         
                Dup_count = 0,   
                Enhance_level = 0,
                Enhance_fail_count = 0
            };
            _context.item_instances.Add(starterItem);

            await _context.SaveChangesAsync();
            return Ok("회원가입 성공");
        }

        private string GenerateJwtToken(Player player)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, player.Id.ToString()), // 유저 ID 저장
                new Claim(ClaimTypes.Name, player.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1), // 토큰 유효 기간
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var player = await _context.players.FirstOrDefaultAsync(u => u.Id == dto.Id);
            if (player == null || !BCrypt.Net.BCrypt.Verify(dto.Password, player.Password))
                return Unauthorized("Invalid ID or password.");

            var token = GenerateJwtToken(player);
            return Ok(new { message = "로그인 성공", token = token, userId = player.Id });
        }

        [HttpPost("guest-login")]
        public async Task<IActionResult> GuestLogin([FromBody] string deviceId)
        {
            var player = await _context.players.FirstOrDefaultAsync(u => u.Id == deviceId);

            if (player == null)
            {
                player = new Player
                {
                    Id = deviceId,
                    Name = "Guest",
                    Username = $"Guest_{Guid.NewGuid().ToString().Substring(0, 5)}",
                    Password = "Guest",
                    Email = ""
                };

                _context.players.Add(player);
                await _context.SaveChangesAsync();
            }

            var token = GenerateJwtToken(player);

            return Ok(new
            {
                message = "게스트 로그인 성공",
                token = token,
                userId = player.Id
            });
        }
    }
}
