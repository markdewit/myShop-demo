using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketservice;
        public BasketController(IBasketService Basketservice)
        {
            this.basketservice = Basketservice;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketservice.GetBasketItems(this.HttpContext);
            return View(model);
        }
        public ActionResult AddToBasket(string Id)
        {

            basketservice.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("index");
        }
        public ActionResult RemoveFromBasket(string Id)
        {

            basketservice.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("index");
            }
        public PartialViewResult BasketSummary() {

            var basketSummary = basketservice.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }

    }
}