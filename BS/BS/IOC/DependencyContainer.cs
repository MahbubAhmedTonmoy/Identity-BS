using BusDomainCore.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitMqBus;
using System;

namespace IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>(sp => {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            //subscriptions
            //services.AddTransient<TransferEventHandler>();
            ////event
            //services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();
            ////Domain Banking Commands
            //services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            //Application
            //services.AddTransient<IAccountService, AccountService>();
            //services.AddTransient<ITransferService, TransferService>();

            //data

            //services.AddTransient<IAccountRepository, AccountRepository>();
            //services.AddTransient<ITransferRepositoy, TransferRepositoy>();
            //services.AddTransient<BankingDBContext>();
            //services.AddTransient<BankingDBContext_Transfer>();
        }
    }
}
