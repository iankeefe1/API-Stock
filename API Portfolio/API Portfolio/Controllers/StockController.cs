using API_Portfolio.Data;
using API_Portfolio.Entities;
using API_Portfolio.Model;
using BCrypt;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace API_Portfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly AppDbContext _context;

        public StockController(AppDbContext context)  // ✅ inject
        {
            _context = context;
        }

        #region GET
        //[HttpGet("GenerateReportStockTransaction")]
        //public async Task<byte[]> GenerateReportStockTransaction(DateTime datefrom, DateTime dateto)
        //{
        //    if (string.IsNullOrEmpty(datefrom.ToString()) || string.IsNullOrEmpty(dateto.ToString()))
        
        //    else
        //    {
        //        var data = await ( 
        //                from 
        //                )
        //        .Join(_context.StockOutDetails,
        //              t => t.StockId,
        //              u => u.Id,
        //              (t, u) => new { t, u })
        //        .Join(_context.Products,
        //              tu => tu.t.ProductId,
        //              p => p.Id,
        //              (tu, p) => new
        //              {
        //                  tu.t.Id,
        //                  User = tu.u.Username,
        //                  Product = p.Name,
        //                  tu.t.Total,
        //                  tu.t.CreatedDate
        //              })
        //        .ToListAsync();
        //    }
        //}

        [HttpGet("GetStockByStockId")]
        public string GetStockByStockId(int stockid)
        {
            if (stockid == 0)
                return "Stock ID Is Not Declared";
            else
            {
                var stocks = _context.Stocks
                .Where(u => u.StockId == stockid)
                .FirstOrDefault();

                if (stocks == null)
                {
                    return "Stock Is Not Found";
                }
                else
                {
                    return "Stock Found";

                }
            }
        }
        #endregion

        #region POST
        [HttpPost("AddStock")]
        public async Task<IActionResult> AddStock(Stock stock)
        {
            if(string.IsNullOrEmpty(stock.StockName))
            {
                return BadRequest("Please Fill the Stock Name");
            }

            if (stock.StockCount == null)
            {
                return BadRequest("Please Fill the Stock Count");
            }

            _context.Add(stock);
            await _context.SaveChangesAsync();

            return Ok("Stock Has Been Added");
        }
        #endregion

        #region PUT
        [HttpPut("EditStock")]
        public async Task<IActionResult> EditStock(string stockname, int stockcount)
        {
            if (string.IsNullOrEmpty(stockname))
            {
                return BadRequest("Please Fill the Stock Name");
            }

            var stocks = _context.Stocks
            .Where(u => u.StockName == stockname)
            .FirstOrDefault();

            if (stocks == null)
            {
                return Ok ("No Stocks Related Found");
            }
            else
            {
                stocks.StockCount = stockcount;
                await _context.SaveChangesAsync();

                return Ok ("Edit Stocks Completed");
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("DeleteStock")]
        public async Task<IActionResult> DeleteStock(string stockname)
        {
            if (string.IsNullOrEmpty(stockname))
                return BadRequest("Stock Name cannot be empty");


            var stock = _context.Stocks
            .Where(u => u.StockName == stockname)
            .FirstOrDefault();


            if (stock == null)
            {
                return BadRequest("No Stocks Found");
            }          
            else
            {
                var stockIn = _context.StockInDetails
                .Where(u => u.StockId == stock.StockId)
                .FirstOrDefault();                

                var stockout = _context.StockOutDetails
                .Where(u => u.StockId == stock.StockId)
                .FirstOrDefault();

                if (stockIn != null)
                {
                    return BadRequest("This Product has been Stock In");
                }
                else if (stockout != null)
                {
                    return BadRequest("This Product has been Stock Out");
                }
                else
                {
                    _context.Remove(stock);
                    _context.SaveChanges();
                    return Ok("Delete Stocks Completed");
                }   
            }
        }
        #endregion
    }
}
