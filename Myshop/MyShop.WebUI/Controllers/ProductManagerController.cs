using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using Myshop.DataAccess.InMemory;



namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        ProductRepository Context;

        public ProductManagerController()
        {
            Context = new ProductRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();
            return View(products);
        }
        
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
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
            return View(p);
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
            Context.Update(pToEdit, Id);
            
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