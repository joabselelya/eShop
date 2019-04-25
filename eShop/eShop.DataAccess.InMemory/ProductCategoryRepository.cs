using eShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();

            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory, string Id)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == Id);

            if (productCategoryToUpdate == null)
            {
                throw new Exception("No product to update!");
            }

            productCategoryToUpdate.Id = productCategory.Id;
            productCategoryToUpdate.Category = productCategory.Category;
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategoryToFind = productCategories.Find(p => p.Id == Id);

            if (productCategoryToFind == null)
            {
                throw new Exception("Product not found!");
            }
            else
            {
                return productCategoryToFind;
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete == null)
            {
                throw new Exception("No product to delete!");
            }
            else
            {
                productCategories.Remove(productCategoryToDelete);
            }
        }

    }
}
