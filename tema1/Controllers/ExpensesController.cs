using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using tema1.Models;
using tema1.Services;
using tema1.ViewModels;

namespace tema1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        
        private IExpenseService expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }

        /// <summary>
        /// Get all expenses
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="type"></param>
        /// <returns>a list of expenses</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        // ? permite unui struct sa ia si valoare null
        public IEnumerable<ExpenseGetModel> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]Models.TypeExpenses? type)
        {
            return expenseService.GetAll(from, to, type);
        }
        /// <summary>
        /// Get an expense by ID
        /// </summary>
        /// <param name="id">The given ID</param>
        /// <returns>The expense with the given ID</returns>
        // GET: api/Expenses/5
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
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
        public void Post([FromBody] Expense expense)
        {
            expenseService.Create(expense);
        }

        /// <summary>
        /// Upsert an expense
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expense"></param>
        /// <returns></returns>
        // PUT: api/Expenses/5
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {
            var result = expenseService.Upsert(id, expense);
            return Ok(result);
        }

        /// <summary>
        /// Deletes an expense by id
        /// </summary>
        /// <param name="id">the id of the expense to be deleted</param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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