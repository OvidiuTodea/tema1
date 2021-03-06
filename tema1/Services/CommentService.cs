﻿using Microsoft.EntityFrameworkCore;
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


            //foreach (Expense e in result)
            //{
            //    e.Comments.ForEach(c =>
            //    {
            //        if (c.Text == null || filter == null)
            //        {
            //            CommentGetModel comment = new CommentGetModel
            //            {
            //                Important = c.Important,
            //                Text = c.Text,
            //                ExpenseId = e.Id

            //            };
            //            resultCommentsNoFilter.Add(comment);
            //        }
            //        else if (c.Text.Contains(filter))
            //        {
            //            CommentGetModel comment = new CommentGetModel
            //            {
            //                Important = c.Important,
            //                Text = c.Text,
            //                ExpenseId = e.Id

            //            };
            //            resultComments.Add(comment);
            //        }
            //    });
        //}

            foreach (Expense expense in result)
            {
                expense.Comments.ForEach(comment =>
                {
                    CommentGetModel newComment = CommentGetModel.FromComment(comment, expense);

                    if (comment.Text == null || filter == null)
                    {
                        resultCommentsNoFilter.Add(newComment);
                    }
                    else if (comment.Text.Contains(filter))
                    {
                        resultComments.Add(newComment);
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
