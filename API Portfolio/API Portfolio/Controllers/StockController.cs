using API_Portfolio.Data;
using API_Portfolio.Entities;
using API_Portfolio.Model;
using BCrypt;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


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
    }
}
