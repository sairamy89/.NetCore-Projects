using Microsoft.AspNetCore.Authorization;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.Extensions.Configuration;  
using Microsoft.IdentityModel.Tokens;  
using System;  
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;  
using System.Text; 
using TestWithProc.Models;

namespace TestWithProc.Controllers
{
    
    [Route("api/[controller]")]  
    [ApiController]  
    public class LoginController : Controller  
    {  
        private IConfiguration _config;  
  
        public LoginController(IConfiguration config)  
        {  
            _config = config;  
        }  
        [AllowAnonymous]  
        [HttpPost]  
        public IActionResult Login([FromBody]UserModel login)  
        {  
            IActionResult response = Unauthorized();  
            var user = AuthenticateUser(login);  
  
            if (user != null)  
            {  
                var tokenString = GenerateJSONWebToken(user);  
                response = Ok(new { token = tokenString });  
            }  
  
            return response;  
        }  
  
        private string GenerateJSONWebToken(UserModel userInfo)  
        {  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  
  
            // var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(1),  
            //  signingCredentials: credentials);
            var utcNow = DateTime.UtcNow;
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], null, expires: utcNow.AddMinutes(20), signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }  
  
        private UserModel AuthenticateUser(UserModel login)  
        {  
            UserModel user = null;  
  
            //Validate the User Credentials  
            //Demo Purpose, I have Passed HardCoded User Information  
            if (login.Username == "Faiz" && login.Password == "Password")  
            {  
                user = new UserModel { Username = "Faiz", Password = "Password" };  
            }  
            return user;  
        }  
    }  
}