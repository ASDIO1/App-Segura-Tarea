using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Data.Entities
{
    public class Product_ComboEntity
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long? Id { get; set; }

        public long ProductId { get; set; }
        public ProductEntity Product { get; set; }

        public long ComboId { get; set; }
        public ComboEntity Combo { get; set; }
    }
}
