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
        [HttpPut("AddStockIn")]
        public async Task<IActionResult> AddStockIn(StockIn stockin)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            List<Transactions> listtransactions = new List<Transactions>();

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
                    else
                    {
                        Transactions transactions = new Transactions();

                        transactions.StockId = stockInDetail.StockId;

                        transactions.StockInId = stockInDetail.StockInId;
                        transactions.StockInDetailId = stockInDetail.StockInDetailId;
                        transactions.StockCount = stockInDetail.StockInCount;

                        transactions.DateTransaction = DateTime.Now;

                        listtransactions.Add(transactions);
                    }
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

        [HttpPut("AddStockOut")]
        public async Task<IActionResult> AddStockOut(StockOut stockOut)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            List<Transactions> listtransactions = new List<Transactions>();

            try
            {
                foreach (var stockoutdetail in stockOut.StockOutDetail)
                {
                    if (stockoutdetail.StockId == 0)
                    {
                        return BadRequest("Stock Not Found");
                    }

                    var stock = await _context.Stocks
                        .FirstOrDefaultAsync(s => s.StockId == stockoutdetail.StockId);

                    if (stock == null)
                    {
                        return BadRequest($"Stock ID {stockoutdetail.StockId} not found");
                    }
                    else
                    {
                        Transactions transactions = new Transactions();

                        transactions.StockId = stockoutdetail.StockId;

                        transactions.StockOutId = stockoutdetail.StockOutId;
                        transactions.StockOutDetailId = stockoutdetail.StockOutDetailId;
                        transactions.StockCount = stockoutdetail.StockOutCount;

                        transactions.DateTransaction = DateTime.Now;

                        listtransactions.Add(transactions);
                    }
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
