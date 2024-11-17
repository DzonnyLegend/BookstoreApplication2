using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;

namespace Common.Interface
{
    [ServiceContract]
    public interface IBank : ITransactionService
    {
        [OperationContract]
        Task<List<string>> ListClients();

        [OperationContract]
        Task EnlistMoneyTransfer(int userID, double amount);

        [OperationContract]
        Task<string> GetValidClient(string user);

    }
}
