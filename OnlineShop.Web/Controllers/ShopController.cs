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
        public ActionResult Index(string searchTerm, int? minimumPrce, int? maximunPrice, int? categoryID, int? sortBy)
        {
            ShopViewModel model = new ShopViewModel();

            model.FeaturedCategories = CategoriesServices.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductsServices.Instance.GetMaximumPrice();

            model.Products = ProductsServices.Instance.SearchProducts(searchTerm, minimumPrce, maximunPrice, categoryID, sortBy);

            model.SortBy = sortBy;

            return View(model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrce, int? maximunPrice, int? categoryID, int? sortBy)
        {
            FilterProductsViewModel model = new FilterProductsViewModel();

            model.Products = ProductsServices.Instance.SearchProducts(searchTerm, minimumPrce, maximunPrice, categoryID, sortBy);

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