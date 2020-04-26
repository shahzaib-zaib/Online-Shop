using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services
{
    public class CategoriesServices
    {
        #region Singleton
        public static CategoriesServices Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesServices();

                return instance;
            }
        }
        private static CategoriesServices instance { get; set; }

        private CategoriesServices()
        {
        }
        #endregion
        public Category GetCategory(int ID)
        {
            using (var context = new OSContext())
            {
                return context.Categories.Find(ID);
            }
        }
        public int GetCategoriesCount(string search)
        {
            using (var context = new OSContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null &&
                         category.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }

        public List<Category> GetAllCategories()
        {
            using (var context = new OSContext())
            {
                return context.Categories.ToList();
            }
        }
        public List<Category> GetCategories()
        {
            using (var context = new OSContext())
            {
                return context.Categories.ToList();
            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new OSContext())
            {
                return context.Categories.Where(x => x.IsFeatured && x.ImageURL != null).ToList();
            }
        }

        public void SaveCategory(Category category)
        {
            using (var context = new OSContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new OSContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int ID)
        {
            using (var context = new OSContext())
            {
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var category = context.Categories.Find(ID);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
