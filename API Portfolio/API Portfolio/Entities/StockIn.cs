using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Portfolio.Entities
{
    [Table("StockIn")]
    public class StockIn
    {
        [Key]
        [Column("StockInId")]
        public int StockInId { get; set; }

        public string StockInSummary { get; set; }

        public string StockInUser { get; set; }
    }
}
