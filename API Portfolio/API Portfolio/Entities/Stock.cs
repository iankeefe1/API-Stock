using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Portfolio.Entities
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        [Column("StockId")]
        public int StockId { get; set; }

        public string? StockName { get; set; }

        public int? StockCount { get; set; }

    }
}
