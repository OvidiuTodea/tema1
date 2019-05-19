using tema1.Models;

namespace tema1.ViewModels
{
    public class ExpenseGetModel
    {
        public string Description { get; set; }
        public double Sum { get; set; }
        public string Location { get; set; }
        public int NumberOfComments { get; set; }

        public static ExpenseGetModel FromExpense(Expense expense)
        {
            return new ExpenseGetModel
            {
                Description = expense.Description,
                Sum = expense.Sum,
                Location = expense.Location,
                NumberOfComments = expense.Comments.Count
            };
        }
    }
}
