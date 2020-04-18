using OnlineShop.Entities;
using OnlineShop.Services;
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
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            productServices.SaveProduct(product);
            return RedirectToAction("ProductTable");
        }
    }
}