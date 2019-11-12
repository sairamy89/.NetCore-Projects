namespace msedclwebApi.Models
{
    public class LoadProfile
    {
        public string serial_number{get; set;}
        public string fromdate{get; set;}
        public string todate{get; set;}
        public int active_energy {get; set;}
        public int reactive_energy {get; set;}
        public int apperent_energy {get; set;}
        public int active_demand {get; set;}
        public int reactive_demand {get; set;}
        public int apperent_demand {get; set;}
        public int rph_volt {get; set;}
        public int yph_volt {get; set;}
        public int bph_volt {get; set;}
        public int rph_lcurr {get; set;}
        public int yph_lcurr {get; set;}
        public int bph_lcurr {get; set;}
        public int rph_acurr {get; set;}
        public int yph_acurr {get; set;}
        public int bph_acurr {get; set;}
        public int rph_rcurr {get; set;}
        public int yph_rcurr {get; set;}
        public int bph_rcurr {get; set;}
    }
}