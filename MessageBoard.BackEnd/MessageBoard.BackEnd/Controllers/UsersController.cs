using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MessageBoard.BackEnd.Models;

namespace MessageBoard.BackEnd.Controllers
{
    public class EditProfileData
    {
        public string FirstName{ get; set; }
        public string LastName { get; set; }
    }
    [Produces("application/json")]
    [Route("Users")]
    public class UsersController : Controller
    {
        private ApiContext _contex;

        public UsersController(ApiContext contex)
        {
            this._contex = contex;
        }
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var user = _contex.Users.SingleOrDefault(c => c.Id == id);
            if (user == null)
                return NotFound("Id not found");
            return Ok(user);
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Get()
        {
            return Ok(GetSecuriedUser());
        }
        [Authorize]
        [HttpPost("me")]
        public IActionResult Post([FromBody]EditProfileData profileData )
        {
            var user = GetSecuriedUser();
            user.FirstName = profileData.FirstName??user.FirstName;
            user.LastName = profileData.LastName ?? user.LastName;
            _contex.SaveChanges();
            return Ok(user);
        }
        User GetSecuriedUser()
        {
            var id = HttpContext.User.Claims.FirstOrDefault().Value;
            return _contex.Users.SingleOrDefault(c => c.Id == id);
        }
    }
}