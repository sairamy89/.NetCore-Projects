using msedclwebApi.Models;
using System.Collections.Generic;

namespace msedclwebApi.Repositories
{
    public interface IDashboardRepository
    {   List<DashboardData> GetDashboardData(string consumer_number, int month);
        List<HTBillsData> GetHTBillsDataByConsumerNumber(string consumerNumber);
        List<ReadingDetails> GetHTMeterDetailsBySerialNumber(string serialNumber, string Date);
        List<LoadProfile> GetHTLoadProfileBySerialNumber(string serialNumber, string FromDate,string ToDate);
        List<ReadingDetailsGauge> GetHTReadingDetailsByConsumerNumber(string consumerNumber, string month, string year);
        List<HTDailyLoad> GetHTDailyLoadByConsumerNumber(string consumerNumber, string month, string year);
    }
}