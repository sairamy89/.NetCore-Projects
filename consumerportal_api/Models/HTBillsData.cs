using System;

namespace msedclwebApi.Models
{
    public class HTBillsData
    {
        public string bill_number {get; set;}
        public int bill_month {get; set;}
        public int bill_amount {get; set;}
        public DateTime bill_date {get; set;}
        public DateTime bill_due_date {get; set;}
        public string last_receipt_number {get; set;}
        public DateTime last_receipt_date {get; set;}
        public int last_month_payment {get; set;}
        public int arrears_amt {get; set;}
        public int bill_year {get; set;}
        public int interest_amt {get; set;}
        public int load_factor {get; set;}
        public int ed_amount {get; set;}
        public int dc_amount {get; set;}
        public int ec_amount {get; set;}
        public int fca_amount {get; set;}
        public int mr_amount {get; set;}
        public int demand_penalty {get; set;}
        public int energy_penalty {get; set;}
        public int pf_penalty {get; set;}
        public int rebate_offered {get; set;}
        public int subsidy_offered {get; set;}
        public int annual_sf {get; set;}
        public int other_penalty {get; set;}
        public int other_charges {get; set;}
        public int slc_demanded {get; set;}
        public int sd_demanded {get; set;}
        public int adjustment_amt {get; set;}
        public int addl_chrg {get; set;}
        public int dpc {get; set;}
        public int unit_consumed {get; set;}
        public int contract_demand_kva {get; set;}
    }
}