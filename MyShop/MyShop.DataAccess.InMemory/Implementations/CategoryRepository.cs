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
    public class CategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Category> categories = new List<Category>();

        public CategoryRepository()
        {
            categories = cache["categories"] as List<Category>;
            if (categories == null)
            {
                categories = new List<Category>(){
                    new Category()
                    {
                        Name ="Book"
                    },
                    new Category()
                    {
                        Name ="Story"
                    }
                };
            }
        }

        public void Comit()
        {
            cache["categories"] = categories;
        }

        public void Insert(Category product)
        {
            categories.Add(product);
        }

        public void Update(Category category)
        {
            var categoryUpdate = categories.FirstOrDefault(x => x.Id == category.Id);
            if (categoryUpdate != null)
            {
                categoryUpdate.Name = category.Name;
            }
            else
            {
                throw new NotFound("Product Not Found");
            }
        }

        public Category FindById(Guid id)
        {
            var category = categories.Find(x => x.Id == id);
            if (category == null)
            {
                throw new NotFound("Product Not Found");
            }
            return category;
        }

        public IEnumerable<Category> Find()
        {
            return categories.AsEnumerable();
        }

        public void Delete(Guid id)
        {
            var category = categories.Find(x => x.Id == id);
            if (category == null)
            {
                throw new NotFound("Category Not Found");
            }
            categories.Remove(category);
        }
    }
}
