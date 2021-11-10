using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infraestructure;
using ShoppingCart.Service.Interfaces;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {

        IProductService _productService;

        public ProductController (IProductService productService)
        {
            _productService = productService;
            //int _userId = AppHttpContext.Current.Session.GetInt32("userId")??0;
            //TempData["userId"] = _userId.ToString();
        }

        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            var products = await _productService.GetAll();

            return View(products);
        }

    }
}
