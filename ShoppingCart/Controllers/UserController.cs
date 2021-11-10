using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infraestructure;
using ShoppingCart.Models;
using ShoppingCart.Service.Interfaces;

namespace ShoppingCart.Controllers
{
    public class UserController : Controller
    {

        IUserService _usertService;
        //public static int _userId;

        public UserController(IUserService usertService)
        {
            _usertService = usertService;
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Login(IFormCollection collection)
        public ActionResult Login(User model)
        {
            try
            {
                var user = _usertService.ValidateUser(model);

                if (user.Id == 0) {

                    AppHttpContext.Current.Session.SetInt32("userId", 0);
                    return RedirectToAction(nameof(Login));
                }
                else {
                    AppHttpContext.Current.Session.SetInt32("userId", user.Id);
                    return RedirectToAction("Index", "Product");
                }
            }
            catch
            {
                return RedirectToAction(nameof(Login));
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32("userId", 0);
            return RedirectToAction(nameof(Login));
        }
    }
}
