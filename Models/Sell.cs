using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeZoneStoreTask.Models
{
    public class Sell
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int quantityS { get; set; }

        [ForeignKey("purchaseId")]
        public Purchase Purchase { get; set; }
        public int purchaseId { get; set; }
    }
}
