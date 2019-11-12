using System;

namespace msedclwebApi.Models
{
    public class ReadingDetailsGauge
    {
        public int billing_pf{get; set;}
        public int load_factor{get; set;}
        public int kwc_unit_kwh {get; set;}
        public int actual_billed_demand {get; set;}
        public int kvac_unit_kvah {get; set;}
        public int rkvac_unit_rkvah {get; set;}
        public int rkvac_unit_rkvah_lead {get; set;}
    }
}