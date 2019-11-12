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
    public class AccountRepository: IAccountRepository
    {
        IConfiguration configuration;

        public AccountRepository (IConfiguration _config)
        {
            configuration = _config;
        }

        public IDbConnection GetConnection()
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("DevConnection").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }

        public List<Consumer> GetConsumerDetailsById(string consumer_number, int month)
        {
            List<Consumer> consumer = new List<Consumer>();
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
                        string query = "STP_CP_DETAILS"; 
                        consumer = conn.Query<Consumer>(query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                    } else {
                         Console.WriteLine("GetConsumerDetailsById: Not able to Open the Connection");
                     }
                } catch (Exception ex) {
                    throw ex;
                }
            return consumer;
        }
    }
}