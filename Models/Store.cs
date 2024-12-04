using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace CodeZoneStoreTask.Models
{
    public class Store
    {
        [Key]
        public int storeId { get; set; }

        [Required]
        public string storeName { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

    }
}
