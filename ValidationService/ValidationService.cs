using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Common.Interface;
using Common;
using Common.Model;
using System.Net;


namespace ValidationService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class ValidationService : StatelessService, IValidationService
    {
        public ValidationService(StatelessServiceContext context)
            : base(context)
        { }

        public async Task<List<string>> GetAllBooks()
        {
            ITransactionCoordinator proxy = ServiceProxy.Create<ITransactionCoordinator>(new Uri("fabric:/BookstoreApplication/TransactionCoordinator"));
            return await proxy.GetAllBooks();
        }

        public async Task<List<string>> GetAllClients()
        {
            ITransactionCoordinator proxy = ServiceProxy.Create<ITransactionCoordinator>(new Uri("fabric:/BookstoreApplication/TransactionCoordinator"));
            return await proxy.GetAllClients();
        }

        public async Task<string?> GetBook(int bookID)
        {
            if (bookID < 0) 
            {
                return null;
            }

            ITransactionCoordinator proxy = ServiceProxy.Create<ITransactionCoordinator>(new Uri("fabric:/BookstoreApplication/TransactionCoordinator"));
            return await proxy.GetBook(bookID);
        }

        public async Task<string?> GetValidClient(string client)
        {
            if (!string.IsNullOrEmpty(client)) 
            {
                return null;
            }
            ITransactionCoordinator proxy = ServiceProxy.Create<ITransactionCoordinator>(new Uri("fabric:/BookstoreApplication/TransactionCoordinator"));
            return await proxy.GetValidClient(client);
        }



        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            long iterations = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
