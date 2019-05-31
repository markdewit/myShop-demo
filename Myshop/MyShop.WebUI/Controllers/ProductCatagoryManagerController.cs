using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using Myshop.DataAccess.InMemory;
using Myshop.Core.contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCatagoryManagerController : Controller
    {
        // GET: ProductCatagoryManager
        IRepository<ProductCatagory> Context;
       

        public ProductCatagoryManagerController(IRepository<ProductCatagory> context)
        {
            this.Context = context;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCatagory> productCatagories = Context.Collection().ToList();
            return View(productCatagories);
        }

        public ActionResult Create()
        {
            ProductCatagory productCatagory = new ProductCatagory();
            return View(productCatagory);
        }
        [HttpPost]
        public ActionResult Create(ProductCatagory productCatagory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCatagory);
            }

            Context.Insert(productCatagory);
            Context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            ProductCatagory p = Context.Find(Id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(ProductCatagory p, string Id)
        {
            ProductCatagory pToEdit = Context.Find(Id);
            if (pToEdit == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            Context.Update(p);
            

            return RedirectToAction("Index");

        }
        public ActionResult Delete(string Id)
        {
            ProductCatagory pToDelete = Context.Find(Id);
            if (pToDelete == null)
            {
                return HttpNotFound();
            }


            return View(pToDelete);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCatagory pToDelete = Context.Find(Id);
            if (pToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Context.Delete(Id);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}