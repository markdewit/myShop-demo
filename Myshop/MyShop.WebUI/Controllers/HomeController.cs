using Myshop.Core.contracts;
using Myshop.DataAccess.InMemory;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> Context;
        IRepository<ProductCatagory> productcategories;

        public HomeController(IRepository<Product> context, IRepository<ProductCatagory> productCategories)
        {
            Context = context;
            productcategories = productCategories;
        }

        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();
            return View(products);
        }
        public ActionResult Details(string Id) {
            Product product = Context.Find(Id);

            if (product == null)
            {
                return HttpNotFound("procukt nicht gefunden");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}