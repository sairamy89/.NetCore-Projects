using System;

namespace msedclwebApi.Models
{
    public class DashboardData
    {
        public string consumer_number {get; set;}
        public string consumer_name {get; set;}
        public string bu {get; set;}
        public string circle_code {get; set;}
        public int bill_month {get; set;}
        public int bill_amount {get; set;}
        public int bill_year {get; set;}
        public int unit_consumed {get; set;}
        public DateTime bill_due_date {get; set;}
    }
}