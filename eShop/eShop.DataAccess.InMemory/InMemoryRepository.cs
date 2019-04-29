using eShop.Core.Contracts;
using eShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;

            if (items == null)
                items = new List<T>();
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t, string Id)
        {
            T tToUpdate = items.Find(i => i.Id == Id);

            if (tToUpdate == null)
                throw new Exception("No " + className + " to update!");

            tToUpdate = t;
        }

        public T Find(string Id)
        {
            T tToFind = items.Find(i => i.Id == Id);

            if (tToFind == null)
                throw new Exception(className + " not found!");

            return tToFind;
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tToDelete = items.Find(i => i.Id == Id);

            if (tToDelete == null)
                throw new Exception("No " + className + " to delete!");

            items.Remove(tToDelete);
        }
    }
}
