using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Portfolio.Entities
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }

        public string? FirstName { get; set; }

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        public string? Telphone { get; set; }

        public string Username { get; set; }

        public string Passwords { get; set; }
    }
}
