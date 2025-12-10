using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Portfolio.Entities
{
    [Table("StockOut")]
    public class StockOut
    {
        [Key]
        [Column("StockOutId")]
        public int StockOutId { get; set; }

        public string StockOutSummary { get; set; }

        public string StockOutUser { get; set; }

        public string StockOutOwner { get; set; }

        public List<StockOutDetail> StockOutDetail { get; set; }
    }
}
