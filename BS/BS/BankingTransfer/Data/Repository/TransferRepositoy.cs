using BankingTransfer.Data.Context;
using BankingTransfer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingTransfer.Data.Repository
{
    public class TransferRepositoy : ITransferRepositoy
    {
        private readonly BankingDBContext_Transfer _context;
        public TransferRepositoy(BankingDBContext_Transfer context)
        {
            _context = context;
        }

        public void Add(TransferLog transferLog)
        {
            _context.Accounts.Add(transferLog);
            _context.SaveChanges();
        }

        public IEnumerable<TransferLog> GetTansferLogs()
        {
            return _context.Accounts;
        }
    }
    public interface ITransferRepositoy
    {
        IEnumerable<TransferLog> GetTansferLogs();
        void Add(TransferLog transferLog);
    }
}
