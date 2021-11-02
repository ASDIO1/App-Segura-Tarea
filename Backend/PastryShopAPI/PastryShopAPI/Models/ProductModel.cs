using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Models
{
    public class ProductModel
    {
        public long? Id { get; set; }
        [Required]
        [StringLength(35, ErrorMessage = "Error {0} Product name is invalid. It should be at most {1} and at least {2}.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(215, ErrorMessage = "Error {0} Product description is invalid. It should be at most {1} and at least {2}.", MinimumLength = 2)]
        public string Description { get; set; }
        [Required]
        public long? Price { get; set; }
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? Rating { get; set; } 
        //[Required]
        public string ImageUrl { get; set; }
        public string ImagePath { get; set; }
        public long CategoryId { get; set; }
        // public List<string> Flavors { get; set; }
    }
}
