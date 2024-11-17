using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public double AccountBalance { get; set; }
        public List<Book> PurchasedBooks { get; set; } = new List<Book>();

        public Customer(long userId, string fullName, double accountBalance)
        {
            UserId = userId;
            FullName = fullName;
            AccountBalance = accountBalance;
        }
        public Customer() { }
    }
}

