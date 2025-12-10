using API_Portfolio.Data;
using API_Portfolio.Entities;
using API_Portfolio.Model;
using BCrypt;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Portfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController :  ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)  // ✅ inject
        {
            _context = context;
        }

        #region GET
        [HttpGet("GetAllTransactions")]
        public async Task<string> GetAllTransactions()
        {
            var transactions = await _context.Transactions.ToListAsync();

            if (transactions.Count == 0)
            {
                return "Transactions Is Not Found";
            }
            else
            {
                return "Transactions Found";

            }           
        }
        #endregion

        #region POST
        [HttpPut("EditStockIn")]
        public async Task<IActionResult> EditStockIn(StockIn stockin)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var stockInDetail in stockin.StockInDetail)
                {
                    if (stockInDetail.StockId == 0)
                    {
                        return BadRequest("Stock Not Found");
                    }

                    var stock = await _context.Stocks
                        .FirstOrDefaultAsync(s => s.StockId == stockInDetail.StockId);

                    if (stock == null)
                    {
                        return BadRequest($"Stock ID {stockInDetail.StockId} not found");
                    }

                    stock.StockCount += stockInDetail.StockInCount;
                }

                // ✅ Save only ONCE
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok("Stocks In Completed");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ex.Message);
            }
        }
        #endregion 
    }
}
