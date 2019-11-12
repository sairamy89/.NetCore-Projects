using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            var currUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if(currUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany  = DateTime.Today.Year - date.Year;
            }

            if(spendingTimeWithCompany > 5)
            {
                return new string[] {"High Time1", "High Time2", "High Time3", "High Time4", "High Time5"};
            } else {
                return new string[] {"value1", "value2", "value3", "value4", "value5"};
            }
        }
        
        
        
        // private readonly ValuesContext _ctx;

        // public ValuesController(ValuesContext context) => _ctx = context;

        
        // [HttpGet]
        // public ActionResult<IEnumerable<Values>> GetValues()
        // {
        //     var records = _ctx.GetValues.FromSql($"exec [dbo].[GetCommands]").ToList();
        //     return records;

        //     // return new string[] {"value1", "value2"};
        // }
        
        // [HttpGet("Id={id}&Price={price}")]
        // public ActionResult<IEnumerable<Values>> GetValue(int id, int price)
        // {
        //     var records = _ctx.GetValues.FromSql($"exec [dbo].[GetCommands] {id}, {price}").ToList();
        //     return records;

        //     // return new string[] {"value1", "value2"};
        // }


        // [HttpGet]
        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

    }
}