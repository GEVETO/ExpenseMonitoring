using ExpenseMonitoring.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMonitoring.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {

        private readonly DataContext DbContext;

        public ExpenseController(DataContext dbContext) 
        {
            DbContext = dbContext;
        }

        //GET    : /api/expenses: Get all expenses
        [HttpGet]
        public async Task<ActionResult<List<Expense>>> GetExpenses()
        {

            //Expense _Expense = new Expense();
            //_Expense.Id = 1;
            //_Expense.Name = "Samp";
            //_Expense.Amount = 100;
            //_Expense.Type = "earned";
            //_Expense.Date = DateTime.Now;

            //List<Expense> _lstExpense = new List<Expense>();
            //_lstExpense.Add(_Expense);
            //return Ok(_lstExpense);
            var _lst = await DbContext.Expenses.ToListAsync();



            return Ok(_lst);
        }


        //GET    : /api/expenses/{id}: Get a specific expense by id
        [HttpGet("{pID}")]
        public async Task<ActionResult<Expense>> GetExpensebyID(int pID)
        {

            var _note = await DbContext.Expenses.FindAsync(pID);

            if (_note == null)
                return BadRequest("Expense ID not found.");

            return Ok(_note);
        }

        //POST   : /api/expenses: Create a new expense(send JSON payload)
        [HttpPost]
        public async Task<ActionResult<List<Expense>>> CreateExpense(Expense pExpense)
        {
            DbContext.Expenses.Add(pExpense);
            await DbContext.SaveChangesAsync();

            return Ok(await DbContext.Expenses.ToListAsync());

        }


        //PUT    : /api/expenses/{id}: Update an existing expense (send JSON payload)
        [HttpPut]
        public async Task<ActionResult<List<Expense>>> UpdateExpense(Expense pExpense)
        {
            var _Expenses = await DbContext.Expenses.FindAsync(pExpense.Id);


            if (_Expenses == null)
                return BadRequest("Note ID not found.");

            _Expenses.Name = pExpense.Name;
            _Expenses.Amount = pExpense.Amount;
            _Expenses.Type = pExpense.Type;
            _Expenses.Date = DateTime.Now;

            await DbContext.SaveChangesAsync();

            return Ok(await DbContext.Expenses.ToListAsync());
        }

        //DELETE: / api / expenses /{ id}: Delete a expense by id
        [HttpDelete("{pID}")]
        public async Task<ActionResult<List<Expense>>> DeleteExpense(int pID)
        {
            var _note = await DbContext.Expenses.FindAsync(pID);


            if (_note == null)
                return BadRequest("Note ID not found.");

            DbContext.Expenses.Remove(_note);
            await DbContext.SaveChangesAsync();

            return Ok(await DbContext.Expenses.ToListAsync());
        }
    }
}
