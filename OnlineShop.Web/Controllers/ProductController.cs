using OnlineShop.Entities;
using OnlineShop.Services;
using OnlineShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsServices productServices = new ProductsServices(); 

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search)
        {
            var products = productServices.GetProducts();
            if (string.IsNullOrEmpty(search) == false)
            {
                products = products.Where(p => p.Name != null  && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoriesServices categoriesServices = new CategoriesServices();
            var categories = categoriesServices.GetCategories();

            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            CategoriesServices categoriesServices = new CategoriesServices();

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = categoriesServices.GetCategory(model.CategoryID);
            productServices.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = productServices.GetProduct(ID);

            return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productServices.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productServices.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }

    }
}