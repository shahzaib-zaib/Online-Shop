using OnlineShop.Services;
using OnlineShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Index(string searchTerm, int? minimumPrce, int? maximunPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();

            model.FeaturedCategories = CategoriesServices.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductsServices.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductsServices.Instance.SearchProductsCount(searchTerm, minimumPrce, maximunPrice, categoryID, sortBy);
            model.Products = ProductsServices.Instance.SearchProducts(searchTerm, minimumPrce, maximunPrice, categoryID, sortBy, pageNo.Value, 10);

            model.Pager = new Pager(totalCount, pageNo);

            return View(model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrce, int? maximunPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            FilterProductsViewModel model = new FilterProductsViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductsServices.Instance.SearchProductsCount(searchTerm, minimumPrce, maximunPrice, categoryID, sortBy);
            model.Products = ProductsServices.Instance.SearchProducts(searchTerm, minimumPrce, maximunPrice, categoryID, sortBy, pageNo.Value, 10);
            model.Pager = new Pager(totalCount, pageNo);
            return PartialView(model);
        }
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];
            if (CartProductsCookie != null)
            {
                //var productIDs = CartProductsCookie.Value;
                //var ids = productIDs.Split('-');
                //List<int> pids = ids.Select(x => int.Parse(x)).ToList();

                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsServices.Instance.GetProducts(model.CartProductIDs);
            }

            return View(model);
        }
    }
}