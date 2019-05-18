using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tema1.Models;

namespace tema1.ViewModels
{
    public class ExpensePostModel
    {
        public string Description { get; set; }
        public double Sum { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }

        //food,
        //utilities,
        //transportation,
        //outing,
        //groceries,
        //clothes,
        //electronics,
        //other
        public static Expense ToExpense(ExpensePostModel expense)
        {
            TypeExpenses typeExpenses = Models.TypeExpenses.other;
            if(expense.Type == "food")
            {
                typeExpenses = Models.TypeExpenses.food;
            }
            else if(expense.Type == "utilities")
            {
                typeExpenses = Models.TypeExpenses.utilities;
            }
            else if (expense.Type == "transportation")
            {
                typeExpenses = Models.TypeExpenses.transportation;
            }
            else if (expense.Type == "transportation")
            {
                typeExpenses = Models.TypeExpenses.transportation;
            }
            else if (expense.Type == "outing")
            {
                typeExpenses = Models.TypeExpenses.outing;
            }
            else if (expense.Type == "groceries")
            {
                typeExpenses = Models.TypeExpenses.groceries;
            }
            else if (expense.Type == "clothes")
            {
                typeExpenses = Models.TypeExpenses.clothes;
            }
            else if (expense.Type == "electronics")
            {
                typeExpenses = Models.TypeExpenses.electronics;
            }

            return new Expense
            {
                Description = expense.Description,
                Sum = expense.Sum,
                Location = expense.Location,
                Date = expense.Date,
                Currency = expense.Currency,
                Type = typeExpenses


            };
        }
    }
}
