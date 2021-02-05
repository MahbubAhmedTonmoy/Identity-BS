using BankingSolution.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Domain.Interface
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
