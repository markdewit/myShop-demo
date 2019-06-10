using Myshop.Core.contracts;
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
        IRepository<Customer> customers;
        IBasketService basketservice;
        IOrderService orderService;

        public BasketController(IBasketService Basketservice, IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.basketservice = Basketservice;
            this.orderService = OrderService;
            this.customers = Customers;
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
        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = customers.Collection().FirstOrDefault(x => x.Email == User.Identity.Name);

            if (customer != null) {

                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FristName = customer.Fristname,
                    SureName = customer.Lastname,
                    zipCode = customer.Zipcode
                };

                return View(order);
            }
            else
            {
                return RedirectToAction("error");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order) {
            var basketItems = basketservice.GetBasketItems(this.HttpContext);
            order.OderStatus = "Order Processed";
            order.Email = User.Identity.Name;

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