using BankingSolution.Domain.Events;
using BusDomainCore.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Domain.EventHandler
{
    public class LoanApproveEventHandler : IEventHandler<LoanApproveEvent>
    {
        public Task Handle(LoanApproveEvent @event)
        {
            Console.WriteLine(@event.Ammount);
            return Task.CompletedTask;
        }
    }
}
