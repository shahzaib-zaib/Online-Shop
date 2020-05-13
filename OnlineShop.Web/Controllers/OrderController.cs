using OnlineShop.Services;
using OnlineShop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(string userID, string status, int? pageNo)
        {
            OrdersViewModel model = new OrdersViewModel();
            model.UserID = userID;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1; //Use this or else Down one
            var pageSize = ConfigurationsService.Instance.PageSize();
            model.Orders = OrderServices.Instance.SearchOrders(userID, status, pageNo.Value, pageSize);

            var totalRecords = OrderServices.Instance.SearchOrdersCount(userID, status);

            model.Pager = new Pager(totalRecords, pageNo, pageSize);
            return View();
        }
    }
}