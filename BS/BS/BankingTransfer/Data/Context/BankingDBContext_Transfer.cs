using BankingTransfer.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingTransfer.Data.Context
{
    public class BankingDBContext_Transfer : DbContext
    {
        public BankingDBContext_Transfer(DbContextOptions options) : base(options)
        {

        }
        public DbSet<TransferLog> Accounts { get; set; }
    }
}
