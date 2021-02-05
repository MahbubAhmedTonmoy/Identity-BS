using BankingSolution.Application.Model;
using BankingSolution.Domain.Commands;
using BankingSolution.Domain.Interface;
using BankingSolution.Domain.Model;
using BusDomainCore.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Application.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;
        private readonly IEventBus bus;

        public AccountService(IAccountRepository repo, IEventBus bus)
        {
            _repo = repo;
            this.bus = bus;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _repo.GetAccounts();
        }

        public void Transfer(TransferAmmount transferAmmount)
        {
            var createTransferCommand = new CreateTransferCommand(
                transferAmmount.FromAccount, transferAmmount.TOAccount, transferAmmount.Balence);
            bus.SandCommand(createTransferCommand);
        }
    }

    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void Transfer(TransferAmmount transferAmmount);
    }
}
