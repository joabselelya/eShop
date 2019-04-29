using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShop.Core.Contracts;
using eShop.Core.Models;
using eShop.DataAccess.InMemory;

namespace eShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> productCategoryContext)
        {
            this.context = productCategoryContext;
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
                return View(productCategory);

            context.Insert(productCategory);
            context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
                return HttpNotFound();

            return View(productCategoryToEdit);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            if (!ModelState.IsValid)
                return View(productCategory);

            context.Update(productCategory, Id);
            context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
                return HttpNotFound();

            return View(productCategoryToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            context.Delete(Id);
            context.Commit();

            return RedirectToAction("Index");
        }
    }
}