using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Portfolio.Entities
{
    [Table("StockOutDetail")]
    public class StockOutDetail
    {
        [Key]
        [Column("StockOutDetailId")]
        public int StockOutDetailId { get; set; }

        public int StockOutId { get; set; }

        public int StockOutCount { get; set; }

        public int StockId { get; set; }
    }
}
