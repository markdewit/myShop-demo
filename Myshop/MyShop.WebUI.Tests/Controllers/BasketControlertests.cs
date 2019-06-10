using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Myshop.Core.contracts;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Services;
using MyShop.WebUI.Tests.Mocks;
using System.Linq;
using MyShop.WebUI.Controllers;
using System.Web.Mvc;
using MyShop.Core.ViewModels;
using System.Security.Principal;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControlertests
    {
       // private ControllerContext httpContext;

        [TestMethod]
        public void canAddBasketItems()
        {
           
            

            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> order = new MockContext<Order>();
            IRepository<Customer> customer = new MockContext<Customer>();

            var httpContext = new MockHttpContext();

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(order);

            var controller = new BasketController(basketService,orderService, customer);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller );
            
            //basketService.AddToBasket(HttpContext, "1");
            controller.AddToBasket("1"); 

            Basket basket = baskets.Collection().FirstOrDefault();

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count());
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);
        }
        public void CanGetSummaryViewModel() {

            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> order = new MockContext<Order>();
            IRepository<Customer> customer = new MockContext<Customer>();

            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 56.00m });
            products.Insert(new Product() { Id = "3", Price = 4.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quanity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quanity = 1 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "3", Quanity = 1 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(order);

            var controller = new BasketController(basketService, orderService, customer);
            var httpContext = new MockHttpContext();

            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(4, basketSummary.Basketcount);
            Assert.AreEqual(80.00m, basketSummary.BasketTotalValue);
        }

        [DataTestMethod]
        public void canCheckoutAndCreateOrder()
        {
           
            IRepository<Product> products = new MockContext<Product>();
            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            IRepository<Basket> baskets = new MockContext<Basket>();
            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quanity = 1, BasketId= basket.Id });
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quanity = 1, BasketId = basket.Id });
            baskets.Insert(basket);



            IBasketService basketService = new BasketService(products, baskets);
            IRepository<Order> orders = new MockContext<Order>();
            IOrderService orderService = new OrderService(orders);
            IRepository<Customer> customers = new MockContext<Customer>();

            customers.Insert(new Customer() { Id = "1", Email="test@test.test", Zipcode="123456"});

            IPrincipal Fakeuser = new GenericPrincipal(new GenericIdentity("test@test.test", "forms"),null);

            var controller = new BasketController(basketService, orderService, customers);
            var httpContext = new MockHttpContext();
            httpContext.User = Fakeuser;
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket")
            {
                Value = basket.Id
            });

            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            Order order = new Order();

            controller.Checkout(order);

            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.BasketItems.Count);

            Order orderInRep = orders.Find(order.Id);
            Assert.AreEqual(2, orderInRep.OrderItems.Count);

        }
        
    }
}
