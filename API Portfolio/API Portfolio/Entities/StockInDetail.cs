using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Portfolio.Entities
{
    [Table("StockInDetail")]
    public class StockInDetail
    {
        [Key]
        [Column("StockInDetailId")]
        public int StockInDetailId { get; set; }

        public int StockInId { get; set; }

        public int StockInCount { get; set; }

        public int StockId { get; set; }
    }
}
