using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tema1.Models;
using tema1.Services;
using tema1.ViewModels;

namespace tema1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        //private ExpensesDbContext context;
        private IExpenseService expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
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
        public IEnumerable<ExpenseGetModel> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]Models.TypeExpenses? type)
        {
            return expenseService.GetAll(from, to, type);
        }

        // GET: api/Expenses/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = expenseService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
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
        public void Post([FromBody] ExpensePostModel expense)
        {
            expenseService.Create(expense);
        }

        // PUT: api/Expenses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {
            var result = expenseService.Upsert(id, expense);
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = expenseService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}