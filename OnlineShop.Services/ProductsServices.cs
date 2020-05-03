﻿using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OnlineShop.Services
{
    public class ProductsServices
    {
        #region Singleton
        public static ProductsServices Instance
        {
            get
            {
                if (instance == null) instance = new ProductsServices();

                return instance;
            }
        }
        private static ProductsServices instance { get; set; }

        private ProductsServices()
        {
        }
        #endregion
        public Product GetProduct(int ID)
        {
            using (var context = new OSContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();
            }
        }
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new OSContext())
            {
                return context.Products.Where(product => IDs.Contains(product.ID)).ToList();
            }
        }
        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 5;// int.Parse(ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);

            using (var context = new OSContext())
            {
                return context.Products.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();

                //return context.Products.Include(x => x.Category).ToList();
            }
        }
        public List<Product> GetLatesProducts(int numberOfProducts)
        {
            using (var context = new OSContext())
            {
                return context.Products.OrderByDescending(x => x.ID).Take(numberOfProducts).Include(x => x.Category).ToList();

            }
        }
        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var context = new OSContext())
            {
                return context.Products.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProductsByCategory(int categoryID, int pageSize)
        {
            using (var context = new OSContext())
            {
                return context.Products.Where(x => x.Category.ID == categoryID).OrderByDescending(x => x.ID).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProducts(string search, int pageNo)
        {
            int pageSize = 3;
            using (var context = new OSContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(product => product.Name != null &&
                         product.Name.ToLower().Contains(search.ToLower()))
                         .OrderBy(x => x.ID)
                         .Skip((pageNo - 1) * pageSize)
                         .Take(pageSize)
                         .Include(x => x.Category)
                         .ToList();
                }
                else
                {
                    return context.Products
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
            }
        }
        public int GetProductsCount(string search)
        {
            using (var context = new OSContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(product => product.Name != null &&
                         product.Name.ToLower().Contains(search.ToLower()))
                         .Count();
                }
                else
                {
                    return context.Products.Count();
                }
            }
        }
        public void SaveProduct(Product product)
        {
            using (var context = new OSContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Unchanged;

                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var context = new OSContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProduct(int ID)
        {
            using (var context = new OSContext())
            {
                //context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                var product = context.Products.Find(ID);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
