using MyShop.Core.Models;
using MyShop.DataAccess.InMemory.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController()
        {
            _productRepository = new ProductRepository();
        }

        // GET: Products
        public ActionResult Index()
        {
            List<Product> products = _productRepository.Find().ToList();
            return View(products);
        }

        public ActionResult Details(Guid id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _productRepository.Insert(product);
            _productRepository.Comit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Guid id, Product requestProduct)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            product.Name = requestProduct.Name;
            product.Price = requestProduct.Price;
            product.Description = requestProduct.Description;
            product.Category = requestProduct.Category;
            product.Image = requestProduct.Image;
            _productRepository.Comit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            _productRepository.Delete(id);
            _productRepository.Comit();
            return RedirectToAction("Index");
        }
    }
}