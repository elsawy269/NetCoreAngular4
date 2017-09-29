using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using MessageBoard.BackEnd.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessageBoard.BackEnd.Controllers
{
    public class JwtPacket
    {
        public string Token{ get; set; }
        public string FirstName{ get; set; }
    }
    public class LoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    [Produces("application/json")]
    [Route("Auth")]
    public class AuthController : Controller
    {
        private ApiContext _contex;

        public AuthController(ApiContext contex)
        {
            this._contex = contex;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginData loginData)
        {
           var user= _contex.Users.SingleOrDefault(c=>c.Email==loginData.Email&&c.Password==loginData.Password);

            if (user==null)
            {
                return NotFound("Email or password incorrect");
            }
            return Ok(CreateJwtPacket(user));

        }

        [HttpPost("register")]
        public JwtPacket Register([FromBody] User user)
        {
            _contex.Users.Add(user);
            _contex.SaveChanges();
            return CreateJwtPacket(user);
        }
        JwtPacket CreateJwtPacket(User user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };
            var signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("THIS IS SECERECT THREE"));
            var signinCredetails = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(claims: claims,signingCredentials: signinCredetails);
            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new JwtPacket { Token = encodeJwt, FirstName = user.FirstName };

        }
    }
}
