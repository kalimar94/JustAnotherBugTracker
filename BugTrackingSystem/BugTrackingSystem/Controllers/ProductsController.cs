using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repositories.Units;
using BugTrackingSystem.Models;
using BugTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Controllers
{
    public class ProductsController : Controller
    {
        private ProductProjectUserUnit unitOfWork;

        public ProductsController()
        {
            unitOfWork = new ProductProjectUserUnit(new ApplicationDbContext());

        }

        // GET: Product
        public ActionResult Index()
        {
            return View(unitOfWork.Products.GetAll("Owner"));
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {
            return View(unitOfWork.Products.GetByID(id));
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var viewModel = new EditProductViewModel
            {
                AvailableOwners = unitOfWork.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id })
            };

            return View(viewModel);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(EditProductViewModel newProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Products.Insert((Product)newProduct);
                    unitOfWork.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {
            var selectedProduct = unitOfWork.Products.Single(x => x.Id == id);

            var viewModel = new EditProductViewModel(selectedProduct)
            {
                AvailableOwners = unitOfWork.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id })
            };

            return View(viewModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, EditProductViewModel editedProduct)
        {
            try
            {
                var toUpdate = unitOfWork.Products.GetByID(id);

                TryUpdateModel(toUpdate);
                unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                unitOfWork.Products.Delete(id);
                unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
