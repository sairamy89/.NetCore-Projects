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
    public class DashboardRepository: IDashboardRepository
    {
        IConfiguration configuration;

        public DashboardRepository (IConfiguration _config)
        {
            configuration = _config;
        }

        public IDbConnection GetConnection()
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("DevConnection").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }

        public List<HTBillsData> GetHTBillsDataByConsumerNumber(string consumerNumber)
        {
            Console.WriteLine(consumerNumber);
            List<HTBillsData> htBillsData = new List<HTBillsData>();
            if(consumerNumber == "")
            {
                return htBillsData;
            }
            try {
                    var dyParam = new OracleDynamicParameters();
                    
                    dyParam.Add("CONSUMER_NUMBERS", consumerNumber, OracleDbType.Varchar2, ParameterDirection.Input);
                    dyParam.Add("v_refcur", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

                    var conn = this.GetConnection();
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    } 
                    if(conn.State == ConnectionState.Open)
                    {
                        Console.WriteLine(consumerNumber);
                        string query = "STP_HT_BILL_DETAILS";
                        
                        htBillsData = conn.Query<HTBillsData>(query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                    } else {
                         Console.WriteLine("GetHTBillsDataByConsumerNumber: Not able to Open the Connection");
                     }
                } catch (Exception ex) {
                    throw ex;
                }
            return htBillsData;
        }

        public List<DashboardData> GetDashboardData(string consumer_number, int month)
        {
            List<DashboardData> consumer = new List<DashboardData>();
            if(consumer_number == "" || month < 0)
            {
                return consumer;
            }
            try {
                    var dyParam = new OracleDynamicParameters();
                    
                    dyParam.Add("CONSUMER_NUMBERS", consumer_number, OracleDbType.Varchar2, ParameterDirection.Input);
                    dyParam.Add("BILL_MONTH", month, OracleDbType.Decimal, ParameterDirection.Input);
                    dyParam.Add("v_refcur", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    var conn = this.GetConnection();
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    } 
                    if(conn.State == ConnectionState.Open)
                    {
                        Console.WriteLine(consumer_number);
                        /* string query = "SELECT CONSUMER_ID,CONSUMER_NUMBER,CONSUMER_NAME,MOBILE_NUMBER,EMAIL_ADDRESS,ADDRESS,METER_ADDRESS_L1,METER_ADDRESS_L2,METER_ADDRESS_L3,BU,PC,SANCTIONED_LOAD,CONTRACT_DEMAND,CONTRACT_DEMAND_KVA,CONNECTED_LOAD,METERING_FLAG,MF_KW,MF_KVA,MF_KWH,MF_KVAH,MF_RKVAH,METER_CT_RATIO,METER_PT_RATIO,CTR,PTR"
                                        + " FROM CONSUMER WHERE CONSUMER_ID = :Consumer_Id";*/
                        string query = "STP_DASHBOARD_DATA"; 
                        consumer = conn.Query<DashboardData>(query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                    } else {
                         Console.WriteLine("GetConsumerDetailsById: Not able to Open the Connection");
                     }
                } catch (Exception ex) {
                    throw ex;
                }
            return consumer;
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
        public List<ReadingDetailsGauge> GetHTReadingDetailsByConsumerNumber(string consumerNumber, string month, string year)
        {
            // Console.WriteLine(consumerNumber);
            // Console.WriteLine(month);
            
            List<ReadingDetailsGauge> readingDetails = new List<ReadingDetailsGauge>();

            if(consumerNumber == "" )
            {
                return readingDetails;
            }
            try {
                    var dyParam = new OracleDynamicParameters();
                    
                    dyParam.Add("CONSUMER_NUMBERS", consumerNumber, OracleDbType.Varchar2, ParameterDirection.Input);
                    dyParam.Add("MONTHS", month, OracleDbType.Varchar2, ParameterDirection.Input);
                    dyParam.Add("YEARS", year, OracleDbType.Varchar2, ParameterDirection.Input);
                    dyParam.Add("v_refcur", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    
                    var conn = this.GetConnection();
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    } 
                    if(conn.State == ConnectionState.Open)
                    {
                        string query = "STP_HTRD_GAUGETABLE"; 

                        readingDetails = conn.Query<ReadingDetailsGauge>(query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                    } else {
                         Console.WriteLine("GetHTReadingDetailsByConsumerNumber: Not able to Open the Connection");
                     }
                } catch (Exception ex) {
                    throw ex;
                }
                Console.WriteLine("Result :" + readingDetails.Count);
            return readingDetails;
        }
        public List<HTDailyLoad> GetHTDailyLoadByConsumerNumber(string consumerNumber, string month, string year)
        {
            // Console.WriteLine(consumerNumber);
            // Console.WriteLine(month);
            
            List<HTDailyLoad> htDailyLoads = new List<HTDailyLoad>();

            if(consumerNumber == "" )
            {
                return htDailyLoads;
            }
            try {
                    var dyParam = new OracleDynamicParameters();
                    
                    dyParam.Add("CONSUMER_NUMBERS", consumerNumber, OracleDbType.Varchar2, ParameterDirection.Input);
                    dyParam.Add("MONTHS", month, OracleDbType.Varchar2, ParameterDirection.Input);
                    dyParam.Add("YEARS", year, OracleDbType.Varchar2, ParameterDirection.Input);
                    dyParam.Add("v_refcur", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    
                    var conn = this.GetConnection();
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    } 
                    if(conn.State == ConnectionState.Open)
                    {
                        string query = "STP_HT_DAILY_LOAD_ANALYSIS"; 

                        htDailyLoads = conn.Query<HTDailyLoad>(query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                    } else {
                         Console.WriteLine("GetHTDailyLoadByConsumerNumber: Not able to Open the Connection");
                     }
                } catch (Exception ex) {
                    throw ex;
                }
                Console.WriteLine("Result :" + htDailyLoads.Count);
            return htDailyLoads;
        }
    }
}