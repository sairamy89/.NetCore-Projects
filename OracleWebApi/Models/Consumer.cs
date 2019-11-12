using System;

namespace OracleWebApi.Models
{
    public class Consumer
    {
        public int CONSUMER_ID {get; set;}
        public int VOLTAGE_LEVEL_ID	{get; set;}
        public string CONSUMER_NUMBER {get; set;}
        public string CONSUMER_NAME	{get; set;}
        public int TRANSFORMER_ID	{get; set;}
        public int LOCATION_ID	{get; set;}
        public string STATUS_CD	{get; set;}
        public int CREATED_BY	{get; set;}
        public DateTime CREATED_DT	{get; set;}
    }
}