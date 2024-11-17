using Common.Model;

namespace ClientFront.Models
{
    public class CustomerViewModel
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public double AccountBalance { get; set; }
        public List<Book> PurchasedBooks { get; set; } 

        public CustomerViewModel(long userId, string fullName, double accountBalance)
        {
            UserId = userId;
            FullName = fullName;
            AccountBalance = accountBalance;
        }

        public CustomerViewModel() { }
    }
}
