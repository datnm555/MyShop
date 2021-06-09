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
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            this.products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Comit()
        {
            cache["products"] = products;
        }

        public void Insert(Product product)
        {
            products.Add(product);
        }

        public void Update(Product product)
        {
            var productUpdate = products.FirstOrDefault(x => x.Id == product.Id);
            if (productUpdate != null)
            {
                productUpdate.Name = product.Name;
                productUpdate.Price = product.Price;
                productUpdate.Description = product.Description;
            }
            else
            {
                throw new NotFound("Product Not Found");
            }
        }


        public Product FindById(Guid id)
        {
            var product = products.Find(x => x.Id == id);
            if (product == null)
            {
                throw new NotFound("Product Not Found");
            }
            return product;
        }

        public IEnumerable<Product> Find()
        {
            return products.AsEnumerable();
        }

        public void Delete(Guid id)
        {
            var product = products.Find(x => x.Id == id);
            if (product == null)
            {
                throw new NotFound("Product Not Found");
            }
            products.Remove(product);
        }

    }
}
