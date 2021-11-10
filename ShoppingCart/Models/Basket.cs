using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal Total { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<BasketDetail> BasketDetails { get; set; }
    }
}
