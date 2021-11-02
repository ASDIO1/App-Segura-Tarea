using PastryShopAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI
{
    public class CategoryEntity
    {
        [Key]
        [Required]
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
        public string ImagePath { get; set; }// Image path passed by the CategoryFormModel in case the CreateForm endpoint was used
        // public string Flavors { get; set; }// Example:  "chocolate, vanilla"
    }
}
