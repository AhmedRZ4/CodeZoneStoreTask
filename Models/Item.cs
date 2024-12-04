using System.ComponentModel.DataAnnotations;

namespace CodeZoneStoreTask.Models
{
    public class Item
    {
        [Key]
        public int itemId { get; set; }

        [Required]
        public string itemName { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
