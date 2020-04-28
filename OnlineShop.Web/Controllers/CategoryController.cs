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
    public class CategoryController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var Categories = CategoriesServices.Instance.GetCategories();
            return View(Categories);
        }

        public ActionResult CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.Pager = new Pager();
            model.Categories = CategoriesServices.Instance.GetCategories();
            if (!string.IsNullOrEmpty(search))
            {
                model.SearchTerm = search;
                model.Categories = model.Categories.Where(a => a.Name != null && a.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView("_CategoryTable", model);
        }

        #region Creation

        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            newCategory.ImageURL = model.ImageURL;
            newCategory.IsFeatured = model.IsFeatured;

            CategoriesServices.Instance.SaveCategory(newCategory);
            return RedirectToAction("CategoryTable");
        }
        #endregion

        #region Updation
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var category = CategoriesServices.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.IsFeatured = category.IsFeatured;

            return PartialView(model);
            
        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoriesServices.Instance.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.IsFeatured = model.IsFeatured;

            CategoriesServices.Instance.UpdateCategory(existingCategory);
            return RedirectToAction("CategoryTable");
        }

        #endregion

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            CategoriesServices.Instance.DeleteCategory(ID);
            return RedirectToAction("CategoryTable");
        }
    }
}