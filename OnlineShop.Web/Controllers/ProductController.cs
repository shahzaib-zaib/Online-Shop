﻿using OnlineShop.Entities;
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
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.PageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1; //Use this or else Down one

            ////similat to above
            //if(pageNo.HasValue)
            //{
            //  if(pageNo.Value > 0)
            //  {
            //      model.PageNo = pageNo.Value;
            //  }
            //  else
            //  {
            //      model.PageNo = 1;
            //  }
            //}
            //else
            //{
            //      model.PageNo = 1;
            //}
            var totalRecords = ProductsServices.Instance.GetProductsCount(search);
            model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, pageSize);

            model.Pager = new Pager(totalRecords, pageNo, pageSize);

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();
            model.AvailableCategories = CategoriesServices.Instance.GetAllCategories();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new Product();
                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Price = model.Price;
                newProduct.Category = CategoriesServices.Instance.GetCategory(model.CategoryID);
                newProduct.ImageURL = model.ImageURL;
                ProductsServices.Instance.SaveProduct(newProduct);
                return RedirectToAction("ProductTable");
            }
            else
            {
                return PartialView(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductsServices.Instance.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = CategoriesServices.Instance.GetAllCategories();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductsServices.Instance.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Category = CategoriesServices.Instance.GetCategory(model.CategoryID);

            //Don't update imageURL if it's empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            ProductsServices.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsServices.Instance.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = ProductsServices.Instance.GetProduct(ID);
            return View(model);
        }

    }
}