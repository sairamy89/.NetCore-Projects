using System;

namespace msedclwebApi.Models
{
    public class ReadingDetails
    {
        public string serial_number{get; set;}
        public string dates{get; set;}
        public int kwh_reading_total {get; set;}
        public int kvah_reading_total {get; set;}
        public int rkvah_reading_total {get; set;}
        public int kw_reading_total {get; set;}
        public int kva_reading_total {get; set;}
        public int rkvah_lead_total {get; set;}
        public DateTime prev_reading_date {get; set;}
    }
}