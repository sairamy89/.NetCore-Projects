using System;

namespace msedclwebApi.Models
{
    public class HTDailyLoad
    {
        public decimal active_energy{get; set;}
        public decimal reactive_energy{get; set;}
        public decimal apperent_energy {get; set;}
        public decimal active_demand {get; set;}
        public decimal reactive_demand {get; set;}
        public decimal apperent_demand {get; set;}
        public decimal rph_volt {get; set;}
        public decimal yph_volt {get; set;}
        public decimal bph_volt {get; set;}
        public decimal r_curr {get; set;}
        public decimal y_curr {get; set;}
        public decimal b_curr {get; set;}
    }
}