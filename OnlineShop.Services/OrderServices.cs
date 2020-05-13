using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OnlineShop.Services
{
    public class OrderServices
    {
        #region Singleton
        public static OrderServices Instance
        {
            get
            {
                if (instance == null) instance = new OrderServices();

                return instance;
            }
        }
        private static OrderServices instance { get; set; }

        private OrderServices()
        {
        }
        #endregion

        public List<Order> SearchOrders(string userID, string status, int pageNo, int pageSize)
        {
            using (var context = new OSContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userID))
                {
                    orders = orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }
                
                return orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int SearchOrdersCount(string userID, string status)
        {
            using (var context = new OSContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userID))
                {
                    orders = orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return orders.Count;
            }
        }
        public Order GetOrderByID(int ID)
        {
            using (var context = new OSContext())
            {
                return context.Orders.Where(x => x.ID == ID).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
            }
        }

        public bool UpdateOrderStatus(int ID, string status)
        {
            using (var context = new OSContext())
            {
                var order = context.Orders.Find(ID);
                order.Status = status;
                //context.Entry(order).State = EntityState.Modified;    //If order status giving error then active this...
                return context.SaveChanges() > 0;
            }
        }
    }
}
