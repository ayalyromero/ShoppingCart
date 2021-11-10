using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTOs
{
    public class BasketResponse
    {
        public bool StatusResponse { get; set; }
        public string Message { get; set; }
        public int NumberItem { get; set; }
    }
}
