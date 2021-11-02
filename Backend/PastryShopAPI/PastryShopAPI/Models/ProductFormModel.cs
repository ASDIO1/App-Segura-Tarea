using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Models
{
    public class ProductFormModel : ProductModel
    {
        public IFormFile Image { get; set; }
    }
}
