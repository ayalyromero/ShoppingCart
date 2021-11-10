using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(80)]
        public string FullName { get; set; }

        [MinLength(5)]
        [MaxLength(20)]
        public string Username { get; set; }

        [MinLength(6)]
        [MaxLength(10)]
        public string Password { get; set; }
    }
}
