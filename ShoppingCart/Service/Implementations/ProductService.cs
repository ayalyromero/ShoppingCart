using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Persistence;
using ShoppingCart.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Service.Implementations
{
    public class ProductService : IProductService
    {

        private readonly ApiContext _context;

        public ProductService(ApiContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Product>> GetAll()
        {
            try
            {
                var products = await _context.Product
                         .Include(u => u.Category)
                         .ToListAsync();

                return products;
            }
            catch (Exception)
            {

                return new List<Product>();
            }
        }
    }
}
