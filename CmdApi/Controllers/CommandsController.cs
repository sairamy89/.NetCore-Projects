using System;
using System.Text;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace CmdApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {   
        private IConfiguration _config;
        // private readonly CommandContext _context;
        // CommandContext context,    
        public CommandsController(IConfiguration config) 
        {
            // _context = context; 
            _config = config;
        } 

        [AllowAnonymous] 
        [HttpPost]
        public IActionResult Login ([FromBody]UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if(user != null)
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

            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                new Claim("DateOfJoing", DateTime.Now.AddYears(-5).ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                // new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddMinutes(5).ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], claims, notBefore: DateTime.UtcNow, expires: DateTime.UtcNow.AddMinutes(2), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            if(login.Username == "Sairam")
            {
                user = new UserModel{Username = "Sairam Y", EmailAddress="sairam.yalamanchi@esyasoft.com"};
            }

            return user;
        }

        // GET:  api/commands
        // [HttpGet]
        // public ActionResult<IEnumerable<Command>> GetCommands()
        // {
        //     return _context.CommandItems;
        // }

        // // GET:  api/commands/1
        // [HttpGet("{id}")]
        // public ActionResult<Command> GetCommandItem(int id) 
        // {
        //     var commandItem = _context.CommandItems.Find(id);

        //     if(commandItem == null)
        //     {
        //         return NotFound();
        //     }
        //     return commandItem;
        // }

        // // POST:     api/commands
        // [HttpPost]
        // public ActionResult<Command> PostCommandItem(Command command)
        // {
        //     _context.CommandItems.Add(command);
        //     _context.SaveChanges();

        //     return CreatedAtAction("GetCommandItem", new Command{Id = command.Id}, command);
        // }

        // // PUT:   api/commands/1
        // [HttpPut("{id}")]
        // public ActionResult PutCommandItem(int Id, Command command) 
        // {
        //     if(Id != command.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(command).State = EntityState.Modified;
        //     _context.SaveChanges();

        //     return NoContent();
        // }

        // // DELETE:    api/commands/1
        // [HttpDelete("{id}")]
        // public ActionResult<Command> DeleteCommandItem (int id)
        // {
        //     var commandItem = _context.CommandItems.Find(id);

        //     if(commandItem == null)
        //     {
        //         return NotFound();
        //     }
        //     _context.CommandItems.Remove(commandItem);
        //     _context.SaveChanges();

        //     return commandItem;
        // }



        // [HttpGet]
        // public ActionResult<IEnumerable<string>> GetString()
        // {
        //     return new string[] {"this", "is", "Coded"};
        // }
    }
}