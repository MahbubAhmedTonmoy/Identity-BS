using BankingTransfer.Data.Repository;
using BankingTransfer.Domain.Events;
using BankingTransfer.Domain.Model;
using BusDomainCore.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingTransfer.Domain.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepositoy _repo;
        public TransferEventHandler(ITransferRepositoy repo)
        {
            //inject bus send more command -> notification
            _repo = repo;
        }
        public Task Handle(TransferCreatedEvent @event)
        {
            _repo.Add(new TransferLog()
            {
                FromAccount = @event.Form,
                ToAccount = @event.To,
                TransferAmmount = @event.Ammount
            });
            return Task.CompletedTask;
        }
    }
}
