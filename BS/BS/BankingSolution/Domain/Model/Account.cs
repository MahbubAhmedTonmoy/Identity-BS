using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Domain.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal AccountBalence { get; set; }
    }
}
