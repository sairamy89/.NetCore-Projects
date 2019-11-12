using Microsoft.AspNetCore.Mvc;
using msedclwebApi.Models;
using System;
using msedclwebApi.Repositories;

namespace msedclwebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController: Controller
    {
        IAccountRepository accountRepository;

        public AccountController(IAccountRepository _accntRepository)
        {
            accountRepository = _accntRepository;
        }

        [HttpGet("{consumer_number}/{month}")]
        // [ActionName("id")]
        public ActionResult GetConsumerMasterDetails(string consumer_number, int month)
        {
            var result = accountRepository.GetConsumerDetailsById(consumer_number, month);

            if(result.Count==0)
            {
                return NotFound(new { message = "There was no data for the Consumer: " + consumer_number });
            } else {
                return Ok(result);
            }
        }
    }
}