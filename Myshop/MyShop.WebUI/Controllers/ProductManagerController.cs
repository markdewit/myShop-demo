using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using Myshop.DataAccess.InMemory;
using MyShop.Core.ViewModels;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        InMemoryRepository<Product> Context;
        InMemoryRepository<ProductCatagory> productcategories;

        public ProductManagerController()
        {
            Context = new InMemoryRepository<Product>();
            productcategories = new InMemoryRepository<ProductCatagory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();
            return View(products);
        }
        
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.productCatagories = productcategories.Collection();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            Context.Insert(product);
            Context.Commit();

            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(string Id) {
            Product p = Context.Find(Id);
            if (p == null) {
                return HttpNotFound();
            }
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.productCatagories = productcategories.Collection();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(Product p,string Id ) {
            Product pToEdit = Context.Find(Id);
            if (pToEdit == null) {
                return HttpNotFound();
            }
            if (!ModelState.IsValid) {
                return View(p);
            }
            Context.Update(pToEdit);
            
            return RedirectToAction("Index");

        }
        public ActionResult Delete(string Id) {
            Product pToDelete = Context.Find(Id);
            if(pToDelete == null)
            {
                return HttpNotFound();
            }
            

            return View(pToDelete);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product pToDelete = Context.Find(Id);
            if (pToDelete == null)
            {
                return HttpNotFound();
            }
            Context.Delete(Id);

            return RedirectToAction("index");
        }
    }
   
   
}