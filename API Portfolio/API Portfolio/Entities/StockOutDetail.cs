using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Portfolio.Entities
{
    [Table("StockOutDetail")]
    public class StockOutDetail
    {
        [Key]
        [Column("StockOutDetailId")]

        [JsonIgnore]
        public int StockOutDetailId { get; set; }

        [JsonIgnore]
        public int StockOutId { get; set; }

        public int StockOutCount { get; set; }

        public int StockId { get; set; }

        [JsonIgnore]   // <- Important (avoid JSON errors)
        public StockOut? StockOut { get; set; }
    }
}
