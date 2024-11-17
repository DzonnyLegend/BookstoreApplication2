using ClientFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientFront.Controllers
{
    public class ClientController : Controller
    {
        private List<CustomerViewModel> clients;

        public ClientController()
        {
            clients = new List<CustomerViewModel>();
        }

        public void AddClient(CustomerViewModel client)
        {
            clients.Add(client);
        }

        public CustomerViewModel? GetClientById(long clientId)
        {
            return clients.FirstOrDefault(c => c.UserId == clientId);
        }

        public List<CustomerViewModel> GetAllClients()
        {
            return clients;
        }

        public bool UpdateClientBalance(long clientId, double newBalance)
        {
            var client = GetClientById(clientId);
            if (client != null)
            {
                client.AccountBalance = newBalance;
                return true;
            }
            return false;
        }
    }
}
