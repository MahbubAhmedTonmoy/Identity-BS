using BusDomainCore.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingTransfer.Domain.Commands
{
    public class LoanApproveCommand : Command
    {
        public int ToAccount { get; set; }
        public int Ammount { get; set; }
    }
    public class CrateLoanCommand : LoanApproveCommand
    {
        public CrateLoanCommand(int toAccount, int ammount)
        {
            ToAccount = toAccount;
            Ammount = ammount;
        }
    }
}
