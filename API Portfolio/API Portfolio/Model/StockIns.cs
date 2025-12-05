using API_Portfolio.Entities;

namespace API_Portfolio.Model
{
    public class StockIns
    {

        public string? StockInSummary { get; set; }

        public string? StockInUser { get; set; }
        public List<StockInDetails> StockInDetails { get; set; }
    }
}
