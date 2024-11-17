using Microsoft.AspNetCore.Mvc;
using Common;
using Common.Model;
using ClientFront.Models;

namespace ClientFront.Controllers
{
    public class BankController : Controller
    {
        private readonly CustomerDatabase _customerDatabase;

        public BankController(CustomerDatabase customerDatabase)
        {
            _customerDatabase = customerDatabase;
        }

        // Prikaz svih računa
        public IActionResult Index()
        {
            var customers = _customerDatabase.Customers.Select(customer => new CustomerViewModel
            {
                UserId = customer.Key,
                FullName = customer.Value.FullName,
                AccountBalance = customer.Value.AccountBalance
            }).ToList();

            return View(customers);
        }

        // Prikaz forme za kreiranje novog računa
        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        // Kreiranje novog računa
        [HttpPost]
        public IActionResult CreateAccount(string fullName, double initialBalance)
        {
            if (ModelState.IsValid)
            {
                long newClientId = _customerDatabase.Customers.Keys.Max() + 1; // Generiše novi ID za korisnika
                var newCustomer = new Customer
                {
                    UserId = newClientId,
                    FullName = fullName,
                    AccountBalance = initialBalance
                };

                _customerDatabase.Customers.Add(newClientId, newCustomer);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Details(long userId)
        {
            if (_customerDatabase.Customers.TryGetValue(userId, out var customer))
            {
                var viewModel = new CustomerViewModel
                {
                    UserId = customer.UserId,
                    FullName = customer.FullName,
                    AccountBalance = customer.AccountBalance,
                    PurchasedBooks = customer.PurchasedBooks
                };

                return View(viewModel);
            }

            return NotFound();
        }

    }
}
