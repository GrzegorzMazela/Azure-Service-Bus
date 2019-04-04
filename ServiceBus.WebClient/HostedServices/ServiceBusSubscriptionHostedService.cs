using Microsoft.Extensions.Hosting;
using ServiceBus.BusinessLogic;
using ServiceBus.Common.Receiver;
using ServiceBus.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.WebClient.HostedServices
{
    public class ServiceBusSubscriptionHostedService : IHostedService, IDisposable
    {
        private LicenceServices licenceServices { get; set; }
        private ITopicReceiver topicReceiver { get; set; }

        public ServiceBusSubscriptionHostedService(ITopicReceiver topicReceiver, LicenceServices licenceServices)
        {
            this.licenceServices = licenceServices;
            this.topicReceiver = topicReceiver;
        }

        public void Dispose()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            topicReceiver.RegisterOnMessageHandlerAndReceiveMessages<Licence>((licence) =>
            {
                licenceServices.AddLicence(licence);
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
