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
    public class StockOutController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly AppDbContext _context;

        public StockOutController(AppDbContext context)  // ✅ inject
        {
            _context = context;
        }

        #region GET
        [HttpGet("GetStockOutByStockId")]
        public string GetStockOutByStockId(int stockoutid)
        {
            if (stockoutid == 0)
                return "Stock Out ID Is Not Declared";
            else
            {
                var stockin = _context.StockOuts
                .Where(u => u.StockOutId == stockoutid)
                .FirstOrDefault();

                if (stockin == null)
                {
                    return "Stock Out Is Not Found";
                }
                else
                {
                    return "Stock Out Found";

                }
            }
        }
        #endregion

        #region POST
        [HttpPost("AddStockOut")]
        public async Task<IActionResult> AddStockOut(StockOut model)
        {
            if (model.StockOutDetail != null)
            {
                foreach (var item in model.StockOutDetail)
                {
                    if (item.StockId <= 0)
                    {
                        return BadRequest("StockId cannot be empty.");
                    }

                    if (item.StockOutCount <= 0)
                    {
                        return BadRequest("StockOutCount must be greater than 0.");
                    }
                    else
                    {
                        _context.Add(model);
                        await _context.SaveChangesAsync();

                        return Ok("DONE");
                    }
                }
            }
            return Ok("DONE");
        }
        #endregion

        #region DELETE
        [HttpDelete("DeleteStockOutById")]
        public async Task<IActionResult> DeleteStockOutById(int id)
        {
            var stockout = await _context.StockOuts
            .Include(s => s.StockOutDetail)
            .FirstOrDefaultAsync(s => s.StockOutId == id);

            if (stockout == null)
                return NotFound("Stock Out not found.");

            _context.Remove(stockout);
            await _context.SaveChangesAsync();

            return Ok("DONE");
        }
        #endregion
    }
}
