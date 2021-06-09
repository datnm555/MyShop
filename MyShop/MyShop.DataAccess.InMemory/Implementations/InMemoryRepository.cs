using MyShop.Core.Models;
using MyShop.DataAccess.InMemory.ExtentionException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory.Implementations
{
    public class InMemoryRepository<T> where T : Base
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

        public void Comit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            var tUpdate = items.FirstOrDefault(x => x.Id == t.Id);
            if (tUpdate != null)
            {
                tUpdate = t;
            }
            else
            {
                throw new Exception($"{className} Product Not Found");
            }
        }

        public T FindById(Guid id)
        {
            var t = items.Find(x => x.Id == id);
            if (t == null)
            {
                throw new Exception($"{className} Product Not Found");
            }
            return t;
        }

        public IEnumerable<T> Find()
        {
            return items.AsEnumerable();
        }

        public void Delete(Guid id)
        {
            var t = items.Find(x => x.Id == id);
            if (t == null)
            {
                throw new Exception($"{className} Product Not Found");
            }
            items.Remove(t);
        }
    }
}

