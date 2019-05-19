using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tema1.Models;
using tema1.ViewModels;

namespace tema1.Services
{
    public interface ICommentService
    {
        IEnumerable<CommentGetModel> GetAll(string filter);
    }
    public class CommentService : ICommentService
    {
        private ExpensesDbContext context;
        public CommentService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CommentGetModel> GetAll(string filter)
        {

            IQueryable<Expense> result = context.Expenses.Include(c => c.Comments);

            List<CommentGetModel> resultComments = new List<CommentGetModel>();
            List<CommentGetModel> resultCommentsNoFilter = new List<CommentGetModel>();

            foreach (Expense m in result)
            {
                m.Comments.ForEach(c =>
                {
                    if (c.Text == null || filter == null)
                    {
                        CommentGetModel comment = new CommentGetModel
                        {
                            Important = c.Important,
                            Text = c.Text,
                            ExpenseId = m.Id

                        };
                        resultCommentsNoFilter.Add(comment);
                    }
                    else if (c.Text.Contains(filter))
                    {
                        CommentGetModel comment = new CommentGetModel
                        {
                            Important = c.Important,
                            Text = c.Text,
                            ExpenseId = m.Id

                        };
                        resultComments.Add(comment);
                    }
                });
            }
            if (filter == null)
            {
                return resultCommentsNoFilter;
            }
            return resultComments;
        }
    }
}
