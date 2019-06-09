using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Myshop.Core.contracts;
using MyShop.Core.Models;
using MyShop.WebUI;
using MyShop.WebUI.Controllers;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            // Arrange
            IRepository<Product> productContext = new Mocks.MockContext<Product>();
            IRepository<ProductCatagory> productcatagoriesContext = new Mocks.MockContext<ProductCatagory>();

            productContext.Insert(new Product());

            HomeController controller = new HomeController(productContext,productcatagoriesContext);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var viewModel = (Core.ViewModels.ProductListViewModel)result.ViewData.Model;
            // Assert
            Assert.AreEqual(1, viewModel.Products.Count());

        }

        //[TestMethod]
        //public void About()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.About() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        //}

        //[TestMethod]
        //public void Contact()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Contact() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}
    }
}
