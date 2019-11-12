using Microsoft.AspNetCore.Mvc;
using msedclwebApi.Models;
using System;
using msedclwebApi.Repositories;
using System.Collections.Generic;

namespace msedclwebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DashboardController: Controller
    {
        IDashboardRepository dashboardRepository;

        public DashboardController(IDashboardRepository _dashbrdRepository)
        {
            dashboardRepository = _dashbrdRepository;
        }

        [HttpGet("{consumerNumber}")]
        public ActionResult GetByConsumerNumber(string consumerNumber)
        {
            //Console.WriteLine(id);
            var result = dashboardRepository.GetHTBillsDataByConsumerNumber(consumerNumber);

            if(result.Count==0)
            {
                return NotFound(new { message = "There was no data for the consumer_Number: " + consumerNumber });
            } else {
                return Ok(result);
            }
        }

        [HttpGet("data/{consumer_number}/{month}")]
        // [ActionName("id")]
        public ActionResult GetDashboardInfo(string consumer_number, int month)
        {
            var result = dashboardRepository.GetDashboardData(consumer_number, month);

            if(result.Count==0)
            {
                return NotFound(new { message = "There was no data for the Consumer: " + consumer_number });
            } else {
                return Ok(result);
            }
        }

        [HttpGet("{serialNumber}/{InputDate}")]
        public ActionResult GetHTReadingDetails(string serialNumber, string InputDate)
        {
            List<ReadingDetails> result = dashboardRepository.GetHTMeterDetailsBySerialNumber(serialNumber,InputDate);

            if(result.Count == 0)
            {
                return NotFound(new { message = "There was no data for the Serial_Number: " + serialNumber });
            } else {
                return Ok(result);
            }
        }

        [HttpGet("{serialNumber}/{FromDate}/{ToDate}")]
        public ActionResult GetLoadProfile(string serialNumber, string FromDate, string ToDate) 
        {   
            List<LoadProfile> result = dashboardRepository.GetHTLoadProfileBySerialNumber(serialNumber,FromDate,ToDate);

            if(result.Count == 0)
            {
                return NotFound(new { message = "There was no data for the Serial_Number: " + serialNumber });
            } else {
                return Ok(result);
            }
        }
        [HttpGet("chart/{consumerNumber}/{month}/{year}")]
        public ActionResult GetHTReadingDetailsForGaugeTable(string consumerNumber, string month, string year)
        {
            List<ReadingDetailsGauge> result = dashboardRepository.GetHTReadingDetailsByConsumerNumber(consumerNumber, month, year);
            Console.WriteLine(result.Count);
            if(result.Count == 0)
            {
                return NotFound(new { message = "There was no data for the Consumer_Number: " + consumerNumber });
            } else {
                return Ok(result);
            }
        }
         [HttpGet("dailyload/{consumerNumber}/{month}/{year}")]
        public ActionResult GetHTDailyLoad(string consumerNumber, string month, string year)
        {
            List<HTDailyLoad> result = dashboardRepository.GetHTDailyLoadByConsumerNumber(consumerNumber, month, year);
            Console.WriteLine(result.Count);
            if(result.Count == 0)
            {
                return NotFound(new { message = "There was no data for the Consumer_Number: " + consumerNumber });
            } else {
                return Ok(result);
            }
        }
    }
}