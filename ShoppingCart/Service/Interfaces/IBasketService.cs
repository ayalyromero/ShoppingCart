using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DTOs;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Service.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetBasketByUser(int userId);
        Task<BasketResponse> AddProducts(BasketDetail model, int userId);
        Task<int> GetProductsInBasket(int userId);
        Task<bool> Delete(int id);
    }
}
