using BusDomainCore.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Domain.Commands
{
    public class TransferCommand : Command
    {
        public int Form { get; protected set; }
        public int To { get; protected set; }
        public decimal Ammount { get; protected set; }
    }
    public class CreateTransferCommand : TransferCommand
    {
        public CreateTransferCommand(int form, int to, decimal ammount)
        {
            Form = form;
            To = to;
            Ammount = ammount;
        }
    }
}
