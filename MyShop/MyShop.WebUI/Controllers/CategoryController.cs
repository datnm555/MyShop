using MyShop.Core.Models;
using MyShop.DataAccess.InMemory.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;


        public CategoryController()
        {
            _categoryRepository = new CategoryRepository();
        }

        // GET: Category
        public ActionResult Index()
        {
            var categories = _categoryRepository.Find().ToList();
            return View(categories);
        }

        public ActionResult Details(Guid id)
        {
            var category = _categoryRepository.FindById(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _categoryRepository.Insert(category);
            _categoryRepository.Comit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            var category = _categoryRepository.FindById(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, Category requestProduct)
        {
            var category = _categoryRepository.FindById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            category.Name = requestProduct.Name;
            _categoryRepository.Comit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            var category = _categoryRepository.FindById(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid id)
        {
            var category = _categoryRepository.FindById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            _categoryRepository.Delete(id);
            _categoryRepository.Comit();
            return RedirectToAction("Index");
        }
    }
}