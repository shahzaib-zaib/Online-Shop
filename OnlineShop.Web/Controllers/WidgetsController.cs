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

            if (isLatesProducts)
            {
                model.Products = ProductsServices.Instance.GetLatesProducts(4);
            }
            else
            {

            }
            return View();
        }

    }
}