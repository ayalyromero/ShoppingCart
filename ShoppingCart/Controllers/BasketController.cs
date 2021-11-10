using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DTOs;
using ShoppingCart.Infraestructure;
using ShoppingCart.Models;
using ShoppingCart.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class BasketController : Controller
    {

        IBasketService _basketService;
        private int _userId;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
            _userId= AppHttpContext.Current.Session.GetInt32("userId")??0;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetBasketByUser()
        {
            try
            {
                var response = await _basketService.GetBasketByUser(_userId);
                return View(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> GetProductsInBasket()
        {
            try
            {
                var response = await _basketService.GetProductsInBasket(_userId);
                return Json(response);
            }
            catch (Exception)
            {
                return Json("");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddProducts(BasketDetail model)
        {
            try
            {
                var response = await _basketService.AddProducts(model, _userId);
                return Json(response);
            }
            catch(Exception ex)
            {
                return Json(new BasketResponse() { StatusResponse = true, Message = ex.Message, NumberItem=0 }); 
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _basketService.Delete(id);
                return RedirectToAction(nameof(GetBasketByUser));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(GetBasketByUser));
            }
        }
    }
}
