using BankingTransfer.Domain.Commands;
using BankingTransfer.Domain.Events;
using BusDomainCore.Bus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankingTransfer.Domain.CommandHandlers
{
    public class LoanApproveCommandHandler : IRequestHandler<CrateLoanCommand, bool>
    {
        private readonly IEventBus bus;

        public LoanApproveCommandHandler(IEventBus bus)
        {
            this.bus = bus;
        }
        public Task<bool> Handle(CrateLoanCommand request, CancellationToken cancellationToken)
        {
            bus.Publish(new LoanApproveEvent(request.ToAccount, request.Ammount));
            return Task.FromResult(true);
        }
    }
}
