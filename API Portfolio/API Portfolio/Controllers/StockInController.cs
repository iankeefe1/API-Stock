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
    public class StockInController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly AppDbContext _context;

        public StockInController(AppDbContext context)  // ✅ inject
        {
            _context = context;
        }

        #region GET
        [HttpGet("GetStockInByStockId")]
        public string GetStockInByStockId(int stockinid)
        {
            if (stockinid == 0)
                return "Stock In ID Is Not Declared";
            else
            {
                var stockin = _context.StockIn
                .Where(u => u.StockInId == stockinid)
                .FirstOrDefault();

                if (stockin == null)
                {
                    return "Stock In Is Not Found";
                }
                else
                {
                    return "Stock In Found";

                }
            }
        }
        #endregion

        #region POST
        [HttpPost("AddStockIn")]
        public async Task<IActionResult> AddStockIn(StockIn model)
        {
            if(model.StockInDetail != null )
            {
                foreach (var item in model.StockInDetail)
                {
                    if (item.StockId <= 0)
                    {
                        return BadRequest("StockId cannot be empty.");
                    }

                    if (item.StockInCount <= 0)
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
        [HttpDelete("DeleteStockInById")]
        public async Task<IActionResult> DeleteStockInById(int id)
        {
            var stockIn = await _context.StockIn
            .Include(s => s.StockInDetail)
            .FirstOrDefaultAsync(s => s.StockInId == id);

            if (stockIn == null)
                return NotFound("StockIn not found.");

            _context.Remove(stockIn);
            await _context.SaveChangesAsync();

            return Ok("DONE");
        }
        #endregion

    }
}
