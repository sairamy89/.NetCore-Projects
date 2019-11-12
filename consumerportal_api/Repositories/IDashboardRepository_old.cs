using msedclwebApi.Models;
using System.Collections.Generic;

namespace msedclwebApi.Repositories
{
    public interface IDashboardRepository_old
    {
        HTBillsData GetHTBillsDataByConsumerNumber(string consumerNumber);
        List<ReadingDetails> GetHTMeterDetailsBySerialNumber(string serialNumber, string Date);
        List<LoadProfile> GetHTLoadProfileBySerialNumber(string serialNumber, string FromDate,string ToDate);
    }
}