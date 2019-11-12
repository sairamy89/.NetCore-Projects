using System.Collections.Generic;
using System;

namespace msedclwebApi.Models
{
    public class Consumer
    {
        public string consumer_number {get; set;}
        public string consumer_name {get; set;}
        public DateTime date_of_connection {get; set;}
        public string consumer_type {get; set;}
        public DateTime pd_td_date {get; set;}
        public string connected_load_kw {get; set;}
        public string contract_demand_kva {get; set;}
        public string tariff_category {get; set;}
        public string part_no {get; set;}
        public string activity_desc {get; set;}
        public string seasonal_consumer_tag {get; set;}
        public string security_deposit_held {get; set;}
        public string bg_lc_amount {get; set;}
        public string tan_no {get; set;}
        public string gstin {get; set;}
        public string make_name {get; set;}
        public string serial_number {get; set;}
        public string express_feeder_flag {get; set;}
        public string substation_number {get; set;}
        public string feeder_number {get; set;}
        public string bill_to_addr_l1 {get; set;}
        public string bill_to_addr_l2 {get; set;}
        public string bill_to_addr_l3 {get; set;}
        public string bill_pin_code {get; set;}
        public string meter_address_L1 {get; set;}
        public string meter_address_L2 {get; set;}
        public string meter_address_L3 {get; set;}
        public string meter_pin_code {get; set;}
        public string mob_conn_com {get; set;}
        
    }
}