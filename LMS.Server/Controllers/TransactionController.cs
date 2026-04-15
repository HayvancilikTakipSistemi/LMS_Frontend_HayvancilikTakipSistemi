using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Server.Repositories;
using LMS.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TransactionController : ControllerBase
    {
        private readonly IGenericRepository<MoneyTransaction> _transactionRepository;

        public TransactionController(IGenericRepository<MoneyTransaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // GET: api/transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoneyTransaction>>> GetAllTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return Ok(transactions);
        }

        // GET: api/transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoneyTransaction>> GetTransactionById(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // POST: api/transaction
        [HttpPost]
        public async Task<ActionResult<MoneyTransaction>> CreateTransaction([FromBody] MoneyTransaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.TransactionID }, transaction);
        }

        // PUT: api/transaction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] MoneyTransaction transaction)
        {
            if (id != transaction.TransactionID)
            {
                return BadRequest();
            }

            var existingTransaction = await _transactionRepository.GetByIdAsync(id);
            if (existingTransaction == null)
            {
                return NotFound();
            }

            existingTransaction.CustomerID = transaction.CustomerID;
            existingTransaction.SaleID = transaction.SaleID;
            existingTransaction.TransactionTypeID = transaction.TransactionTypeID;
            existingTransaction.Amount = transaction.Amount;
            existingTransaction.TransactionDate = transaction.TransactionDate;
            existingTransaction.Description = transaction.Description;

            _transactionRepository.Update(existingTransaction);
            await _transactionRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _transactionRepository.Remove(transaction);
            await _transactionRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
