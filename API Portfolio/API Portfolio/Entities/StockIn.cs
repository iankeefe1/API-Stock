//using API_Portfolio.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Portfolio.Entities
{
    [Table("StockIn")]
    public class StockIn
    {
        [Key]
        [Column("StockInId")]

        [JsonIgnore]   // <- Important (avoid JSON errors)
        public int StockInId { get; set; }   // ✅ KEEP THIS

        public string? StockInSummary { get; set; }
        public string? StockInUser { get; set; }

        public List<StockInDetail> StockInDetail { get; set; }
    }
}
