using Microsoft.AspNetCore.Mvc;
using Common;
using ClientFront.Models;
using System.Transactions;

namespace ClientFront.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookDatabase _bookDatabase;
        private readonly CustomerDatabase _customerDatabase;

        public BooksController(CustomerDatabase customerDatabase, BookDatabase bookDatabase)
        {
            _customerDatabase = customerDatabase;
            _bookDatabase = bookDatabase;
        }

        public IActionResult Index()
        {
            var books = _bookDatabase.Books.Select(kvp => new BookViewModel
            {
                BookId = kvp.Value.BookId,
                Title = kvp.Value.Title,
                Author = kvp.Value.Author,
                Price = kvp.Value.Price,
                Genre = kvp.Value.Genre,
                Quantity = kvp.Value.Quantity
            }).ToList();


            return View(books);
        }

        public ActionResult Details(long id)
        {
            // Prikazuje detalje o odabranoj knjizi
            if (_bookDatabase.Books.TryGetValue(id, out var book))
            {
                var viewModel = new BookViewModel
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price,
                    Genre = book.Genre,
                    Quantity = book.Quantity
                };
                return View(viewModel);
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult Buy(long id)
        {
            if (_bookDatabase.Books.TryGetValue(id, out var book))
            {
                var viewModel = new BookViewModel
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price,
                    Genre = book.Genre,
                    Quantity = book.Quantity
                };
                return View(viewModel);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Buy(long userId, long bookId, uint quantity)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    if (_customerDatabase.Customers.TryGetValue(userId, out var customer) &&
                        _bookDatabase.Books.TryGetValue(bookId, out var book))
                    {

                        if (book.Quantity < quantity)
                        {
                            TempData["Message"] = "Nema dovoljno knjiga na stanju za kupovinu.";
                            TempData["MessageType"] = "error";
                            return RedirectToAction("Index", "Books");
                        }

                        double totalPrice = (double)book.Price * quantity;

                        if (customer.AccountBalance >= totalPrice)
                        {
                            customer.AccountBalance -= totalPrice;
                            book.Quantity -= quantity;

                            for (int i = 0; i < quantity; i++)
                            {
                                customer.PurchasedBooks.Add(book);
                            }

                            transaction.Complete();

                            TempData["Message"] = "Kupovina uspešna!";
                            TempData["MessageType"] = "success";
                            return RedirectToAction("Index", "Books");
                        }
                        else
                        {
                            TempData["Message"] = "Nedovoljno sredstava na računu.";
                            TempData["MessageType"] = "error";
                            return RedirectToAction("Index", "Books");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = $"Greška prilikom obrade transakcije: {ex.Message}";
                    TempData["MessageType"] = "error";
                    return RedirectToAction("Index", "Books");
                }
            }

            // Dodajemo povratnu vrednost u slučaju da nijedan od gore navedenih blokova ne završi.
            TempData["Message"] = "Neuspešna kupovina. Korisnik ili knjiga nisu pronađeni.";
            TempData["MessageType"] = "error";
            return RedirectToAction("Index", "Books");
        }


        [HttpPost]
        public ActionResult CompletePurchase(BookViewModel model)
        {
            if (_bookDatabase.Books.TryGetValue(model.BookId, out var book))
            {
                if (book.Quantity >= model.Quantity)
                {
                    book.Quantity -= model.Quantity;

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Nema dovoljno knjiga na stanju.");
                }
            }
            return View("Buy", model);
        }

        public IActionResult Payment()
        {
            return View();
        }
    }
}
