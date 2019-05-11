using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tema1.Models
{
    //DbContext = unit of work sau o colectie de repositoriuri
    public class ExpensesDbContext : DbContext
    {
        public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options)
        {
        }

        // un DbSet este un repository si o tabela din baza de date
        public DbSet<Expense> Expenses { get; set; }
    }
}
