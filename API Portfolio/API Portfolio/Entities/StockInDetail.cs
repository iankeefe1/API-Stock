using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Portfolio.Entities
{
    [Table("StockInDetail")]
    public class StockInDetail
    {
        [Key]
        [Column("StockInDetailId")]
        [JsonIgnore]   // <- Important (avoid JSON errors)
        public int StockInDetailId { get; set; }

        [JsonIgnore]   // <- Important (avoid JSON errors)
        public int StockInId { get; set; }

        public int StockInCount { get; set; }

        public int StockId { get; set; }

        [JsonIgnore]   // <- Important (avoid JSON errors)
        public StockIn? StockIn { get; set; }
    }
}
