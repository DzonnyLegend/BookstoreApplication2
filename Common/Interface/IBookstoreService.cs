using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Common.Interface
{
    [ServiceContract]
    public interface IBookstoreService : ITransactionService
    {
        [OperationContract]
        Task<Dictionary<long, Book>?> GetAvailableBooks();

        [OperationContract]
        Task<bool> EnlistPurchase(int bookID, uint count, int customerId);

        [OperationContract]
        Task<double?> GetItemPrice(int bookID);

        [OperationContract]
        Task<string?> GetBook(int bookID);

        [OperationContract]
        Task<List<string>> GetAllBooks();
    }
}
