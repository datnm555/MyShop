using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public ProductManagerController()
        {
            _productRepository = new ProductRepository();
            _categoryRepository = new CategoryRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            var productViewModel = _productRepository.Find().ToList();
            return View(productViewModel);
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
            var productViewModel = new ProductViewModel();
            productViewModel.Product = new Product();
            productViewModel.Categories = _categoryRepository.Find();
            return View(productViewModel);
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
            var productViewModel = new ProductViewModel();
            productViewModel.Product = product;
            productViewModel.Categories = _categoryRepository.Find();
            return View(productViewModel);
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
