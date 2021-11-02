using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Data.Entities
{
    public class ProductEntity
    {
        [Key]
        [Required]
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? Price { get; set; }
        public int? Rating { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePath { get; set; }

        // NAVIGATION PROPERTIES
        [ForeignKey("CategoryId")]
        public virtual CategoryEntity Category { get; set; } // 1 to many
        public ICollection<Product_ComboEntity> Product_Combos { get; set; } // many to many
    }
}
