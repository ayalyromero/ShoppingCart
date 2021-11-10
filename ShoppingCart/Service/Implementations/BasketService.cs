using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTOs;
using ShoppingCart.Models;
using ShoppingCart.Persistence;
using ShoppingCart.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Service.Implementations
{
    public class BasketService : IBasketService
    {

        private readonly ApiContext _context;

        public BasketService(ApiContext context)
        {
            _context = context;
        }

        public async Task<BasketResponse> AddProducts(BasketDetail model, int userId)
        {
            BasketResponse response = new BasketResponse();
            int basketId = 0;
            try
            {
                var basket =  await _context.Basket.Where(_ => _.UserId == userId).FirstOrDefaultAsync();

                if (basket!=null) {

                    basketId = basket.Id;

                    var product = _context.Product.Where(_=>_.Id == model.ProductId).FirstOrDefault();

                    if (product != null)
                    {

                        model.BasketId = basket.Id;
                        model.Price = product.Price;
                        response.StatusResponse = true;

                        basket.Total = basket.Total + (model.Price * model.Quantity);

                    }
                    else {
                        response.StatusResponse = false;
                        response.Message = "Error adding product to cart";
                        response.NumberItem = 0;
                        return response;
                    }

                    _context.BasketDetail.Add(model);
                    Console.WriteLine($"[ITEM ADDED TO SHOPPING CART]: Added[<{ model.CreatedDate.ToShortDateString()}>], {userId}, {model.ProductId}, {model.Quantity}, Price[€ {model.Price}]");
                }
                else{

                    var product = _context.Product.Where(_=>_.Id == model.ProductId).FirstOrDefault();

                    if (product != null)
                    {
                        Basket basketNew = new Basket() { 
                            CreatedDate = DateTime.Now,
                            UserId = userId,
                            Total=product.Price*model.Quantity,
                            BasketDetails = new List<BasketDetail>() { 
                                new BasketDetail(){ 
                                    CreatedDate = DateTime.Now,
                                    Price= product.Price,
                                    ProductId= model.ProductId,
                                    Quantity = model.Quantity
                                }
                            }
                        };

                        response.StatusResponse = true;

                        _context.Basket.Add(basketNew);

                        basketId = basketNew.Id;
                        Console.WriteLine($"[BASKET CREATED]: Created[<{basketNew.CreatedDate.ToShortDateString()}>], {userId}");
                        Console.WriteLine($"[ITEM ADDED TO SHOPPING CART]: Added[<{ model.CreatedDate.ToShortDateString()}>], {userId}, {model.ProductId}, {model.Quantity}, Price[€ {model.Price}]");
                    }
                    else
                    {
                        response.StatusResponse = false;
                        response.Message = "Error adding product to cart";
                        response.NumberItem = 0;
                        return response;
                    }
                }

                _context.SaveChanges();
                response.NumberItem = _context.BasketDetail.Where(_ => _.BasketId == basketId).Sum(_ => _.Quantity);

            }
            catch (Exception ex)
            {
                response.StatusResponse = false;
                response.Message = ex.Message;
                response.NumberItem = 0;
            }
            return response;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var basketDetail = await _context.BasketDetail
                                 .Where(_ => _.Id == id)
                                 .FirstOrDefaultAsync();

                _context.BasketDetail.Remove(basketDetail);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Basket> GetBasketByUser(int userId)
        {
            try
            {
                var basket = await _context.Basket
                         .Include(_ => _.BasketDetails)
                         .ThenInclude(_ => _.Product)
                         .Where(_ => _.UserId == userId)
                         .FirstOrDefaultAsync();
                
                if(basket!=null)
                    basket.Total = basket.BasketDetails.Sum(_ => _.Quantity*_.Price);

                return basket;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> GetProductsInBasket(int userId)
        {
            try
            {
                var numberItem = await _context.BasketDetail
                    .Include(_=>_.Basket)
                    .Where(_ => _.Basket.UserId == userId).SumAsync(_ => _.Quantity);

                return numberItem;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
