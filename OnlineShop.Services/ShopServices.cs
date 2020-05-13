using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services
{
    public class ShopServices
    {
        #region Singleton
        public static ShopServices Instance
        {
            get
            {
                if (instance == null) instance = new ShopServices();

                return instance;
            }
        }
        private static ShopServices instance { get; set; }

        private ShopServices()
        {
        }
        #endregion
        public int SaveOrder(Order Order)
        {
            using (var context = new OSContext())
            {
                context.Orders.Add(Order);
                return context.SaveChanges();
            }
        }
    }
}
