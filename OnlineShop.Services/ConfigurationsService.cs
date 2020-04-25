using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services
{
    public class ConfigurationsService
    {
        //public static ConfigurationsService ClassObject
        //{
        //    get
        //    {
        //        if (privateInMemoryObject == null) privateInMemoryObject = new ConfigurationsService();

        //        return privateInMemoryObject;
        //    }
        //}
        //private static ConfigurationsService privateInMemoryObject { get; set; }

        //private ConfigurationsService()
        //{
        //}
        public Config GetConfig(string key)
        {
            using(var context = new OSContext())
            {
                return context.Configurations.Find(key);
            }
        }
    }
}
