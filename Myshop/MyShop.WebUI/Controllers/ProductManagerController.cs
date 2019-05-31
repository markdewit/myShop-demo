using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using Myshop.DataAccess.InMemory;
using MyShop.Core.ViewModels;
using Myshop.Core.contracts;
using System.IO;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        IRepository<Product> Context;
        IRepository<ProductCatagory> ProductCatagories;

        public ProductManagerController(IRepository<Product> context, IRepository<ProductCatagory> productCategories)
        {
            Context = context;
            ProductCatagories = productCategories;
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
            viewModel.productCatagories = ProductCatagories.Collection();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {

                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                Context.Insert(product);
                Context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product p = Context.Find(Id);
            if (p == null)
            {
                return HttpNotFound();
            }
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = p;
            viewModel.productCatagories = ProductCatagories.Collection();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(Product p, string Id, HttpPostedFileBase file)
        {

            Product pToEdit = Context.Find(Id);

            if (pToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(p);
                }
                if (file != null)
                {
                    pToEdit.Image = p.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + pToEdit.Image);
                }
                pToEdit.Category = p.Category;
                pToEdit.Description = p.Description;
                pToEdit.Name = p.Name;
                pToEdit.Price = p.Price;

                Context.Commit();

                return RedirectToAction("Index");

            }
        }

        public ActionResult Delete(string Id)
        {
            Product pToDelete = Context.Find(Id);
            if (pToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(pToDelete);
            }
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
            else
            {
                Context.Delete(Id);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
   
}