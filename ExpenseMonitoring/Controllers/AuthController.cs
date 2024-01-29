using ExpenseMonitoring.DTO;
using ExpenseMonitoring.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace ExpenseMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration pconfiguration)
        {
            _configuration = pconfiguration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto pUserDto)
        {
            CreatePasswordHash(pUserDto.Password, out byte[] pPasswordHash, out byte[] pPasswordSalt);

            user.UserName = pUserDto.UserName;
            user.PasswordHash = pPasswordHash;
            user.PasswordSalt = pPasswordSalt;


            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto pUserDto)
        {
            if (user.UserName != pUserDto.UserName)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(pUserDto.Password,user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string _strToken = CreateToken(user);

            return Ok(_strToken);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> 
            { 
            new Claim(ClaimTypes.Name, user.UserName)
            
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
           
            return jwt;
        }

        private void CreatePasswordHash(string pPassword, out byte[] pPasswordHash, out byte[] pPasswordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                pPasswordSalt = hmac.Key;
                pPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pPassword));
            }
        }

        private bool VerifyPasswordHash(string pPassword, byte[] pPasswordHash, byte[] pPasswordSalt)
        {
            using (var hmac = new HMACSHA512(pPasswordSalt))
            {
                var _computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pPassword));
                return _computedHash.SequenceEqual(pPasswordHash);
            }
        }
    }
}
