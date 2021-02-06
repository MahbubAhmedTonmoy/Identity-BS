using BankingTransfer.Application.DTO;
using BankingTransfer.Data.Repository;
using BankingTransfer.Domain.Commands;
using BankingTransfer.Domain.Model;
using BusDomainCore.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingTransfer.Application.Service
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepositoy _repo;
        private readonly IEventBus _bus;

        public TransferService(ITransferRepositoy repo, IEventBus bus)
        {
            _repo = repo;
            _bus = bus;
        }

        public void ApproveLoan(Loan loan)
        {
            var createLoanApproveCommand = new CrateLoanCommand(loan.ToAccount, loan.Ammount);
            _bus.SandCommand(createLoanApproveCommand);
        }

        public IEnumerable<TransferLog> GetTansferLogs()
        {
            return _repo.GetTansferLogs();
        }
    }
    public interface ITransferService
    {
        IEnumerable<TransferLog> GetTansferLogs();
        void ApproveLoan(Loan loan);
    }
}
