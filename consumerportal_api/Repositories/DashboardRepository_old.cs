using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using msedclwebApi.Models;
using System.Collections.Generic;
using msedclwebApi.LogConfig;
using msedclwebApi.Oracle;

namespace msedclwebApi.Repositories
{
    public class DashboardRepository_old: IDashboardRepository_old
    {
        IConfiguration configuration;

        public DashboardRepository_old (IConfiguration _config)
        {
            configuration = _config;
        }

        public IDbConnection GetConnection()
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("DevConnection").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }

        public HTBillsData GetHTBillsDataByConsumerNumber(string consumerNumber)
        {
            Console.WriteLine(consumerNumber);
            HTBillsData htBillsData = new HTBillsData();
            if(consumerNumber == "")
            {
                return htBillsData;
            }
            try {
                    var conn = this.GetConnection();
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    } 
                    if(conn.State == ConnectionState.Open)
                    {
                        Console.WriteLine(consumerNumber);
                        string query = "SELECT CONSUMER_NUMBER, BILL_NUMBER, BILL_AMOUNT, ED_AMOUNT, DC_AMOUNT, EC_AMOUNT,"
                                        + "FCA_AMOUNT, MR_AMOUNT,DEMAND_PENALTY,ENERGY_PENALTY,PF_PENALTY,REBATE_OFFERED,"
                                        + "SUBSIDY_OFFERED,ANNUAL_SF,OTHER_PENALTY,OTHER_CHARGES,SLC_DEMANDED,SD_DEMANDED,"
                                        + "ADJUSTMENT_AMT,ADDL_CHRG,DPC,BILL_MONTH,BILL_YEAR,ARREARS_AMT,INTEREST_AMT,BILL_DUE_DATE,"
                                        + "UNIT_CONSUMED FROM MBC_LTIP_HT_BILL_DATA WHERE CONSUMER_NUMBER= :CONSUMER_NUMBER"; 
                        
                        htBillsData = conn.Query<HTBillsData>(query, new { CONSUMER_NUMBER = consumerNumber }).FirstOrDefault();
                    } else {
                         Console.WriteLine("GetHTBillsDataByConsumerNumber: Not able to Open the Connection");
                     }
                } catch (Exception ex) {
                    throw ex;
                }
            return htBillsData;
        }
        public List<ReadingDetails> GetHTMeterDetailsBySerialNumber(string serialNumber, string Date)
        {
            // Console.WriteLine(serialNumber);
            // Console.WriteLine(Date);
            
            List<ReadingDetails> readingDetails = new List<ReadingDetails>();

            if(serialNumber == "" && Date == "")
            {
                return readingDetails;
            }
            try {
                    var conn = this.GetConnection();
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    } 
                    if(conn.State == ConnectionState.Open)
                    {
                        // Console.WriteLine(consumerNumber);
                        string query = "SELECT KWH_READING_TOTAL, KVAH_READING_TOTAL, RKVAH_READING_TOTAL, KW_READING_TOTAL,KVA_READING_TOTAL,RKVAH_LEAD_TOTAL,PREV_READING_DATE "
                                        + "FROM READIN_DETAILS WHERE SERIAL_NUMBER= :SERIAL_NUMBER  AND  Trunc(PREV_READING_DATE) < :Dates"; 

                        readingDetails = conn.Query<ReadingDetails>(query, new { SERIAL_NUMBER = serialNumber , DATES = Date }).ToList();
                    } else {
                         Console.WriteLine("GetHTLoadProfileBySerialNumber: Not able to Open the Connection");
                     }
                } catch (Exception ex) {
                    throw ex;
                }
            return readingDetails;
        }
        public List<LoadProfile> GetHTLoadProfileBySerialNumber(string serialNumber, string FromDate,string ToDate)
        {
            // Console.WriteLine(serialNumber);
            // Console.WriteLine(FromDate);
            // Console.WriteLine(ToDate);
            
            List<LoadProfile> loadProfile = new List<LoadProfile>();

            if(serialNumber == "" && FromDate == "" && ToDate == "")
            {
                return loadProfile;
            }
            try {
                    var conn = this.GetConnection();
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    } 
                    if(conn.State == ConnectionState.Open)
                    {
                        // Console.WriteLine(consumerNumber);
                        string query = "SELECT ACTIVE_ENERGY, REACTIVE_ENERGY, APPERENT_ENERGY, ACTIVE_DEMAND, REACTIVE_DEMAND, APPERENT_DEMAND, RPH_VOLT, "
                                      +"YPH_VOLT, BPH_VOLT, RPH_LCURR, YPH_LCURR, BPH_LCURR, RPH_ACURR, YPH_ACURR, BPH_ACURR, RPH_RCURR, YPH_RCURR, BPH_RCURR "
                                        + "FROM LOAD_PROFILE WHERE SERIAL_NUMBER=:SERIAL_NUMBER AND Trunc(DAYPROFILE_DATE) >= :FromDate AND Trunc(DAYPROFILE_DATE) <= :ToDate"; 

                        loadProfile = conn.Query<LoadProfile>(query, new { SERIAL_NUMBER = serialNumber , FROMDATE = FromDate, TODATE = ToDate }).ToList();
                    } else {
                         Console.WriteLine("GetHTLoadProfileBySerialNumber: Not able to Open the Connection");
                     }
                } catch (Exception ex) {
                    throw ex;
                }
            return loadProfile;
        }
    }
}