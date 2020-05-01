using OnlineShop.Services;
using OnlineShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatesProducts)
        {
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();
            model.IsLatestProducts = isLatesProducts;

            if (isLatesProducts)
            {
                model.Products = ProductsServices.Instance.GetLatesProducts(4);
            }
            else
            {
                model.Products = ProductsServices.Instance.GetProducts(1, 8);
            }
            return PartialView(model);
        }

    }
}