using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeZoneStoreTask.Models
{
    public class Purchase
    {
        [Key]
        public int purchaseId { get; set; }
        [Required]
        public int quantityP { get; set; }
        [ForeignKey("itemId")]
        public Item Item { get; set; }
        public int itemId { get; set; }
        [ForeignKey("storeId")]
        public Store Store { get; set; }
        public int storeId { get; set; }
        public ICollection<Sell> Sells { get; set; }

    }
}
