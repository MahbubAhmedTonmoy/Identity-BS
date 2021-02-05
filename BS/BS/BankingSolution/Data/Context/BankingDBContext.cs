using BankingSolution.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Data.Context
{
    public class BankingDBContext : DbContext
    {
        public BankingDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
    }
}
