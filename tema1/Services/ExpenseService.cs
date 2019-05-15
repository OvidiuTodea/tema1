﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tema1.Models;

namespace tema1.Services
{

    public interface IExpenseService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<Expense> GetAll(DateTime? from=null, DateTime? to=null, Models.TypeExpenses? type=null);
        Expense Create(Expense expense);
        Expense Upsert(int id, Expense expense);
        Expense Delete(int id);
        Expense GetById(int id);

    }

    public class ExpenseService : IExpenseService
    {
        private ExpensesDbContext context;

        public ExpenseService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public Expense Create(Expense expense)
        {
            context.Expenses.Add(expense);
            context.SaveChanges();
            return expense;
        }

        public Expense Delete(int id)
        {
            var existing = context.Expenses.Include(e => e.Comments).FirstOrDefault(e => e.Id == id);
            if (existing == null)
            {
                return null;
            }
            context.Expenses.Remove(existing);
            context.SaveChanges();
            return existing;
        }

        public IEnumerable<Expense> GetAll(DateTime? from=null, DateTime? to=null, TypeExpenses? type=null)
        {
            IQueryable<Expense> result = context.Expenses.Include(c => c.Comments);
            if (from == null && to == null && type == null)
            {
                return result;
            }
            if (from != null)
            {
                result = result.Where(e => e.Date >= from);
            }
            if (to != null)
            {
                result = result.Where(e => e.Date <= to);
            }
            if (type != null)
            {
                result = result.Where(e => e.Type.Equals(type));
            }
            return result;
        }

        public Expense GetById(int id)
        {
            return context.Expenses
                .Include(e => e.Comments)
                .FirstOrDefault(e => e.Id == id);
        }

        public Expense Upsert(int id, Expense expense)
        {
            var existing = context.Expenses.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (existing == null)
            {
                context.Expenses.Add(expense);
                context.SaveChanges();
                return expense;
            }
            expense.Id = id;
            context.Expenses.Update(expense);
            context.SaveChanges();
            return expense;
        }
    }
}
