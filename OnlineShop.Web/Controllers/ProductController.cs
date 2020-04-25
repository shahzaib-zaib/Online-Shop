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
        //ProductsServices productServices = new ProductsServices();
        CategoriesServices categoriesServices = new CategoriesServices();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNo)
        {
            pageNo = pageNo.HasValue ? pageNo : 1;

            ProductSearchViewModel model = new ProductSearchViewModel();
            model.Products = ProductsServices.Instance.GetProducts(pageNo.Value);
            if (string.IsNullOrEmpty(search) == false)
            {
                model.SearchTerm = search;
                model.Products = model.Products.Where(p => p.Name != null  && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = categoriesServices.GetCategories();
            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = categoriesServices.GetCategory(model.CategoryID);
            ProductsServices.Instance.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = ProductsServices.Instance.GetProduct(ID);

            return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            ProductsServices.Instance.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsServices.Instance.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }

    }
}