using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingTransfer.Application.DTO
{
    public class Loan
    {
        public int ToAccount { get; set; }
        public int Ammount { get; set; }
    }
}
