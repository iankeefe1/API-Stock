using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Portfolio.Entities
{
    [Table("Transactions")]
    public class Transactions
    {
        [Key]
        [Column("TransactionsID")]
        public int TransactionsID { get; set; }

        public int StockId { get; set; }

        public int? StockInId { get; set; }

        public int? StockOutId { get; set; }

        public int? StockInDetailId { get; set; }

        public int? StockOutDetailId { get; set; }

        public int? StockCount { get; set; }

        public DateTime? DateTransaction { get; set; }
    }
}
