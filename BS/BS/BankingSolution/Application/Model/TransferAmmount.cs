using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Application.Model
{
    public class TransferAmmount
    {
        public int FromAccount { get; set; }
        public int TOAccount { get; set; }
        public decimal Balence { get; set; }
    }
}
