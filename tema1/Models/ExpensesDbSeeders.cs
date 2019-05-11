using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tema1.Models
{
    public class ExpensesDbSeeders
    {
        public static void Initialize(ExpensesDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any flowers.
            if (context.Expenses.Any())
            {
                return;   // DB has been seeded
            }

            context.Expenses.AddRange(
                new Expense
                {
                    
                     Description = "cadou",
                     Sum = 100,
                     Location = "Cluj",
                     Date = DateTime.Now,
                     Currency = "USD", 
                     Type = TypeExpenses.clothes

                },
                new Expense
                {
                    Description = "cort",
                    Sum = 200,
                    Location = "Cluj",
                    Date = DateTime.Now,
                    Currency = "RON",
                    Type = TypeExpenses.outing

                }
            );
            context.SaveChanges();  // commit transaction
        }
    }
}

