using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryShopAPI.Exceptions
{
    public class InvalidOperationItemException : Exception
    {
        public InvalidOperationItemException(string message)
            :base(message)
        {
        }
    }
}
