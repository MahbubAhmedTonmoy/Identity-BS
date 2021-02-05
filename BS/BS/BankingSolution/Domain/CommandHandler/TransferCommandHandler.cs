using BankingSolution.Domain.Commands;
using BankingSolution.Domain.Events;
using BusDomainCore.Bus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankingSolution.Domain.CommandHandler
{
    
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus bus;

        public TransferCommandHandler(IEventBus bus)
        {
            this.bus = bus;
        }

        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            bus.Publish(new TransferCreatedEvent(request.Form, request.To, request.Ammount));
            return Task.FromResult(true);
        }
    }
}
