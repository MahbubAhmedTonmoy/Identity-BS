using BankingSolution.Data.Context;
using BankingSolution.Domain.Interface;
using BankingSolution.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDBContext _context;

        public AccountRepository(BankingDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts;
        }

        IEnumerable<Account> IAccountRepository.GetAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
