using Myshop.Core.contracts;
using Myshop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCatagory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCatagory> productCategoriesContext)
        {
            context = productContext;
            productCategories = productCategoriesContext;
        }

        public ActionResult Index(string Catagory=null)
        {
            List<Product> products;
            List<ProductCatagory> categories = productCategories.Collection().ToList();
            if (Catagory == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(x => x.Category == Catagory).ToList();
            }
            ProductListViewModel Vmodel = new ProductListViewModel();

            Vmodel.Products = products;
            Vmodel.Catagories = categories;
            


            return View(Vmodel);
        }
        public ActionResult Details(string Id) {
            Product product = context.Find(Id);

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