using BusDomainCore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Domain.Events
{
    public class LoanApproveEvent : Event
    {
        public int ToAccount { get; set; }
        public int Ammount { get; set; }
        public LoanApproveEvent(int toAccount, int ammount)
        {
            ToAccount = toAccount;
            Ammount = ammount;
        }
    }
    
}
