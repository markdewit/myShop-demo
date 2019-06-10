using MyShop.Core.Contracts;
using MyShop.Core.Models;
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
        IOrderService orderService;

        public BasketController(IBasketService Basketservice, IOrderService OrderService)
        {
            this.basketservice = Basketservice;
            this.orderService = OrderService;
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
        public ActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(Order order) {
            var basketItems = basketservice.GetBasketItems(this.HttpContext);
            order.OderStatus = "Order Processed";

            //process pament

            order.OderStatus = "Payment Processed";
            orderService.CreateOder(order, basketItems);
            basketservice.ClearBasket(this.HttpContext);


            return RedirectToAction("thankyou", new { OrderId = order.Id });
        }
        public ActionResult thankyou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }


    }
}