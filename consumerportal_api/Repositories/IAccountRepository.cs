using msedclwebApi.Models;
using System.Collections.Generic;

namespace msedclwebApi.Repositories
{
    public interface IAccountRepository
    {
         List<Consumer> GetConsumerDetailsById(string consumer_number, int month);
    }
}