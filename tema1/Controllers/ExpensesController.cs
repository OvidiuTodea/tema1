using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tema1.Models;

namespace tema1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private ExpensesDbContext context;
        public ExpensesController(ExpensesDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all expenses
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /expenses
        ///     {
        ///        "id": 7,
    ///                 "description": "cort nou cu comentariu",
   ///                   "sum": 2000,
    ///                 "location": "Cluj",
    ///                 "date": "2019-05-11T17:07:29.4468654",
   ///                  "currency": "RON",
   ///                  "type": 1,
   ///                  "comments": [
    ///                                {
    ///                                  "id": 1,
     ///                                 "text": "primul text",
     ///                                 "important": true
     ///                                },
     ///                                {
      ///                                 "id": 2,
     ///                                  "text": "al doilea text",
     ///                                  "important": false
     ///                                 }
  ///                                ]
        ///     }
        ///
        /// </remarks>
        /// <param name="from">Optional, filter by minimum Date</param>
        /// <param name="to">Optional, filter by maximum Date</param>
        /// <param name="type">Optional, filter by Type</param>
        /// <returns>A list of expenses objects</returns>
        [HttpGet]
        // ? permite unui struct sa ia si valoare null
        public IEnumerable<Expense> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]Models.TypeExpenses? type)
        {
            IQueryable<Expense> result = context.Expenses.Include(c => c.Comments);
            if (from == null && to == null && type==null)
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

        // GET: api/Expenses/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var existing = context.Expenses.
                Include(c => c.Comments).
                FirstOrDefault(e => e.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            return Ok(existing);
        }

        /// <summary>
        /// Add an expense
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST /expenses
        ///     {
        ///        "id": 7,
        ///                 "description": "cort nou cu comentariu",
        ///                   "sum": 2000,
        ///                 "location": "Cluj",
        ///                 "date": "2019-05-11T17:07:29.4468654",
        ///                  "currency": "RON",
        ///                  "type": 1,
        ///                  "comments": [
        ///                                {
        ///                                  "id": 1,
        ///                                 "text": "primul text",
        ///                                 "important": true
        ///                                },
        ///                                {
        ///                                 "id": 2,
        ///                                  "text": "al doilea text",
        ///                                  "important": false
        ///                                 }
        ///                                ]
        ///     }
        ///
        /// </remarks>
        /// <param name="expense">The expenses to add.</param>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public void Post([FromBody] Expense expense)
        {
            //if (!ModelState.IsValid)
            //{

            //}
            context.Expenses.Add(expense);
            context.SaveChanges();
        }

        // PUT: api/Expenses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {
            var existing = context.Expenses.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (existing == null)
            {
                context.Expenses.Add(expense);
                context.SaveChanges();
                return Ok(expense);
            }
            expense.Id = id;
            context.Expenses.Update(expense);
            context.SaveChanges();
            return Ok(expense);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Expenses.Include(e => e.Comments).FirstOrDefault(e => e.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            context.Expenses.Remove(existing);
            context.SaveChanges();
            return Ok();
        }
    }
}