using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Data.Entities
{
    public class ComboEntity
    {
        [Key]
        [Required]
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? Price { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePath { get; set; }

        // NAVIGATION PROPERTIES (for many to many relationship)
        public ICollection<Product_ComboEntity> Product_Combos { get; set; }
    }
}
