using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class BasketDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Basket))]
        public int BasketId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public Basket Basket { get; set; }
        public Product Product { get; set; }
    }
}
