using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Models.Combos
{
    public class ComboFormModel : ComboModel
    {
        public IFormFile Image { get; set; }
    }
}
