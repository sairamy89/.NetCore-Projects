using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TestWithProc.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging; 

namespace TestWithProc.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController: ControllerBase
    {
        IEnumerable<Customer> _cst ;
         private CustomerContext customerContext;
         readonly ILogger<CustomerController> _log;
        public CustomerController(CustomerContext context,ILogger<CustomerController> log)
        {
            customerContext=context;
             _log = log;
        }

        // // Passing Sp with Multiple parameter
        //[Authorize]
        [AllowAnonymous]
        [HttpGet ("{city}/{Address}")]
        [ActionName("city/Address")]
        public ActionResult<IEnumerable<Customer>> Get(string city,string Address)
        {
           
            var result= customerContext.cust.FromSql($"exec [dbo].[SelectAllCustomers] {city},{Address}" ).ToList();
            return result;

        }




    // Passing Sp with Multiple parameter
        [Authorize]
        [HttpGet]
        [Route("GetCust")]
        [ActionName("city2/Address2")]
         public IActionResult GetCustDetails(string city,string Address)
        {
            try
            {
             String name1 = string.Empty;
             String name2 = string.Empty;
                var headers1 = Request.Headers;
                var headers2 = Request.Headers;
                if (headers1.ContainsKey("city") && headers2.ContainsKey("Address"))
                {
                    name1 = headers1["city"];
                    name2 = headers1["Address"];
                }
             _cst = customerContext.cust.FromSql($"exec [dbo].[SelectAllCustomers] {name1},{name2}" ).ToList();
            //_log.LogInformation("successfully executed");
            //return Ok(_cst);
            }

            catch(Exception ex)
            {               
                _log.LogInformation($"Something went wrong :{ex}"); 
                 return StatusCode(500, "Internal server error");
            }
           return Ok(_cst); 
        }
        
        
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<IEnumerable<Customer>> Post([FromBody] Customer _customer)
        {
            if (_customer == null)
            {
                return BadRequest("No Record.");
            }
 
           var _c = customerContext.Database.ExecuteSqlCommand($"exec [dbo].[SP_InsertData] {_customer.Customer_id},{_customer.Age},{_customer.Customer_name},{_customer.Address}, {_customer.City},{_customer.DateOfBirth}");
           // customerContext.SaveChanges();
            Console.WriteLine(_c);     
           if(_c==1)
            {
                 return Ok("Added Sucessfully");
            }
            else 
            {
                 _log.LogInformation(500, "Internal server error"); 
                  return BadRequest("Pass Correct value");
            }          
        }


        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var _c = customerContext.Database.ExecuteSqlCommand($"exec [dbo].[SP_DeleteCustData] {id}");
            Console.WriteLine(_c);     
           if(_c==1)
            {
                return Ok("Record DELETED Sucessfully");
            }
            else 
            {
                 _log.LogInformation("The record couldn't be Deleted,Pass Correct Value");     
                 return NotFound("The record couldn't be Deleted.");
            }
       
        }



        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Customer _customer)
        {
            // if( ! ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }
            
           var _c = customerContext.Database.ExecuteSqlCommand($"exec [dbo].[SP_UpdateCustData] {id},{_customer.Age}, {_customer.Customer_name}, {_customer.Address}, {_customer.City}, {_customer.DateOfBirth}");
           // customerContext.SaveChanges();   
            Console.WriteLine(_c);
            if(_c==1)
            {
                return Ok("Record Updated Sucessfully");
            }
            else 
            {
                 _log.LogInformation("Record Not Found corresponding to the value passed");  
                return StatusCode(404, "Record Not Found corresponding to the value passed");
            }
        }
            //return Ok("success");
        //    try
        //    {   
        //         customerContext.SaveChanges();
        //    } 
        //     catch(DbUpdateConcurrencyException)
        //     {
        //     //    if(!CustomerExists(id)) 
        //     //    {
        //     //         return NotFound();
        //     //    }      
        //     //    else
        //     //    {
        //     //        throw;
        //     //    }
        


        // [Authorize]
        // [Authorize(Policy = "Admin")]
        // [HttpGet]
        // [Route("api/admin")]
        // [ActionName("city/Address")]
        //  public IActionResult Get(string city,string Address)
        // {
        //      String name1 = string.Empty;
        //      String name2 = string.Empty;
        //         var headers1 = Request.Headers;
        //         var headers2 = Request.Headers;
        //         if (headers1.ContainsKey("city") && headers2.ContainsKey("Address"))
        //         {
        //             name1 = headers1["city"];
        //             name2 = headers1["Address"];
        //         }
        //     IEnumerable<Customer> _cst = customerContext.cust.FromSql($"exec [dbo].[SelectAllCustomers] {name1},{name2}" ).ToList();
        //     return Ok(_cst);
        // }



        // [Authorize(Policy = "Admin")]
        // [HttpGet]
        // public ActionResult<IEnumerable<Customer>> Get()
        // {
           
        //     var result= customerContext.cust.FromSql($"exec [dbo].[SelectAllCustomersByCity]").ToList();
        //     return result;

       
        // // }
        // ~CustomerController()
        // {
        //     customerContext.Dispose();
        // }
        
    }
}